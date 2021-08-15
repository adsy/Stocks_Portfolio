using AutoMapper;
using Domain.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Data;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models;
using Services.Models.Crypto;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Repository.CryptoRepository
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CoinGeckoApiSettings _coinGeckoApiSettings;
        private readonly CoinMarketCapApiSettings _coinMarketcapApiSettings;
        private readonly HttpClient _coinGeckoClient;
        private readonly HttpClient _coinMarketCapClient;

        public CryptoRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpClientFactory httpClientFactory, IOptions<CoinGeckoApiSettings> coinGeckoApiSettings, IOptions<CoinMarketCapApiSettings> coinMarketcapApiSettings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _coinGeckoApiSettings = coinGeckoApiSettings.Value ?? throw new ArgumentNullException(nameof(coinGeckoApiSettings));
            _coinMarketcapApiSettings = coinMarketcapApiSettings.Value ?? throw new ArgumentNullException(nameof(coinMarketcapApiSettings));

            _coinGeckoClient = _httpClientFactory.CreateClient(_coinGeckoApiSettings.ClientName);
            _coinGeckoClient.DefaultRequestHeaders.Add(_coinGeckoApiSettings.GetHostHeader, _coinGeckoApiSettings.GetHostHeaderValue);
            _coinGeckoClient.DefaultRequestHeaders.Add(_coinGeckoApiSettings.GetKeyHeader, _coinGeckoApiSettings.GetKeyHeaderValue);

            _coinMarketCapClient = _httpClientFactory.CreateClient(_coinMarketcapApiSettings.ClientName);
            _coinMarketCapClient.DefaultRequestHeaders.Add(_coinMarketcapApiSettings.GetKeyHeaderKey, _coinMarketcapApiSettings.GetKeyHeaderValue);
        }

        public async Task<Response> AddCryptoToDbAsync(CryptocurrencyDTO crypto)
        {
            var fnResult = new Response
            {
                StatusCode = 200,
                Error = false
            };

            var cryptoObj = _mapper.Map<Cryptocurrency>(crypto);

            await _unitOfWork.Cryptocurrencies.Insert(cryptoObj);

            await _unitOfWork.Save();

            return fnResult;
        }

        public async Task<Response<CryptoChartData>> GetChartDataAsync(string id)
        {
            var fnResult = new Response<CryptoChartData>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var uri = _coinGeckoApiSettings.BaseClient + string.Format(_coinGeckoApiSettings.GetCoinChartDataUri, id);

                var response = await _coinGeckoClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));

                if (response.StatusCode != HttpStatusCode.OK)
                    return fnResult;

                var content = await response.Content.ReadAsStringAsync();

                var chartResponse = JsonConvert.DeserializeObject<CryptoChartResponse>(content);

                var chartData = chartResponse.Prices.Select(q => new ChartData
                {
                    timestampLong = Convert.ToInt64(q[0]),
                    price = Convert.ToDouble(q[1])
                }).ToList();

                foreach (var data in chartData)
                {
                    var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    start = start.AddSeconds(data.timestampLong / 1000).ToLocalTime();
                    data.time = start.ToString("g");
                }

                fnResult.Data = new CryptoChartData { PriceChartData = chartData };

                fnResult.StatusCode = (int)HttpStatusCode.OK;

                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.Message = e.Message;
                return fnResult;
            }
        }

        public async Task<Response<CryptoPortfolio>> GetCryptoPortfolioAsync()
        {
            var fnResult = new Response<CryptoPortfolio>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var cryptocurrenies = await _unitOfWork.Cryptocurrencies.GetAll();

                var cryptoPortfolio = new CryptoPortfolio();

                var queryString = "";

                foreach (var crypto in cryptocurrenies)
                {
                    var coinInfo = _mapper.Map<CoinInfo>(crypto);

                    if (!cryptoPortfolio.Cryptocurrencies.ContainsKey(coinInfo.Name))
                    {
                        cryptoPortfolio.Cryptocurrencies.Add(coinInfo.Name, new CryptoProfile
                        {
                            CoinList = new List<CoinInfo>
                        {
                            coinInfo
                        },
                            CoinCount = 1
                        });
                        queryString += coinInfo.Name + ",";
                    }
                    else
                    {
                        var profile = cryptoPortfolio.Cryptocurrencies[coinInfo.Name];
                        profile.CoinList.Add(coinInfo);
                        profile.CoinCount += 1;
                        cryptoPortfolio.Cryptocurrencies[coinInfo.Name] = profile;
                    }
                }

                var ids = queryString.Remove(queryString.Length - 1);

                var prices = await GetCryptoValuesAsync(ids);

                if (prices.StatusCode != (int)HttpStatusCode.OK)
                    throw new Exception("There was an exception thrown getting crypto values while creating crypto portfolio.");

                foreach (var value in prices.Data)
                {
                    var coinProfile = cryptoPortfolio.Cryptocurrencies[value.Name];

                    coinProfile.CoinName = value.Name;

                    coinProfile.FullName = value.FullName;

                    coinProfile.CurrentPrice = value.Price;

                    double avgPrice = 0;

                    foreach (var entry in coinProfile.CoinList)
                    {
                        entry.CurrentPrice = value.Price;

                        entry.CurrentValue = entry.Amount * entry.CurrentPrice;

                        coinProfile.CurrentValue += entry.CurrentValue;

                        entry.Profit = entry.CurrentValue - entry.TotalCost;

                        coinProfile.TotalProfit += entry.Profit;

                        coinProfile.TotalCost += entry.TotalCost;

                        coinProfile.TotalAmount += entry.Amount;

                        avgPrice += entry.TotalCost;
                    }

                    coinProfile.TotalCost = Math.Round(coinProfile.TotalCost, 2);
                    coinProfile.TotalProfit = Math.Round(coinProfile.TotalProfit, 2);
                    coinProfile.CurrentValue = Math.Round(coinProfile.CurrentValue, 2);
                    coinProfile.CurrentPrice = Math.Round(coinProfile.CurrentPrice, 5);
                    coinProfile.TotalAmount = Math.Round(coinProfile.TotalAmount, 3);

                    coinProfile.AvgPrice = Math.Round(avgPrice / coinProfile.TotalAmount, 3);
                }

                fnResult.StatusCode = (int)HttpStatusCode.OK;
                fnResult.Data = cryptoPortfolio;
                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.Message = e.Message;
                return fnResult;
            }
        }

        public async Task<Response<CryptoSummaryData>> GetCryptoSummaryDataAsync(string id)
        {
            var fnResult = new Response<CryptoSummaryData>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var uri = _coinGeckoApiSettings.BaseClient + string.Format(_coinGeckoApiSettings.GetCoinSummaryDataUri, id);

                var response = await _coinGeckoClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));

                if (response.StatusCode != HttpStatusCode.OK)
                    return fnResult;

                var content = await response.Content.ReadAsStringAsync();

                var chartResponse = JsonConvert.DeserializeObject<CryptoSummaryResponse>(content);

                fnResult.Data = new CryptoSummaryData
                {
                    High24H = double.Parse(chartResponse.Market_Data.High_24h["aud"]),
                    Low24H = double.Parse(chartResponse.Market_Data.Low_24h["aud"]),
                    MarketCapChange24H = double.Parse(chartResponse.Market_Data.Market_Cap_Change_24h),
                    MarketCapChangePercentage24H = double.Parse(chartResponse.Market_Data.Market_Cap_Change_Percentage_24h),
                    MarketCapRank = double.Parse(chartResponse.Market_Cap_Rank),
                    PriceChangePercentage24H = double.Parse(chartResponse.Market_Data.Price_Change_Percentage_24h)
                };

                fnResult.StatusCode = (int)HttpStatusCode.OK;

                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.Message = e.Message;
                return fnResult;
            }
        }

        public async Task<Response<IEnumerable<CryptoValue>>> GetCryptoValuesAsync(string ids)
        {
            var fnResult = new Response<IEnumerable<CryptoValue>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            try
            {
                var cryptoTickers = ids.Split(",");

                var uri = _coinMarketcapApiSettings.BaseClient + string.Format(_coinMarketcapApiSettings.GetCoinQuoteDataUri);

                var response = await _coinMarketCapClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));

                if (response.StatusCode != HttpStatusCode.OK)
                    return fnResult;

                var content = await response.Content.ReadAsStringAsync();

                var requestBody = JObject.Parse(content);

                var cryptoValueList = new List<CryptoValue>();

                foreach (var ticker in cryptoTickers)
                {
                    if (requestBody.SelectToken($"data.{ticker}.quote.AUD.price") != null)
                    {
                        var cryptoTicker = new CryptoValue
                        {
                            Name = ticker,
                            FullName = (string)requestBody.SelectToken($"data.{ticker}.name"),
                            Price = (double)requestBody.SelectToken($"data.{ticker}.quote.AUD.price")
                        };
                        cryptoValueList.Add(cryptoTicker);
                    }
                }
                fnResult.StatusCode = (int)HttpStatusCode.OK;
                fnResult.Data = cryptoValueList;

                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.Message = e.Message;
                return fnResult;
            }
        }

        public async Task<Response> RemoveCryptoFromDbAsync(CryptocurrencyDTO crypto)
        {
            var fnResult = new Response
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var cryptoObject = await _unitOfWork.Cryptocurrencies.GetAll(q => q.Name == crypto.Name);

                if (!cryptoObject.Any())
                {
                    fnResult.Message = $"No matching cryptocurrency found for {crypto.Name}.";
                    return fnResult;
                }

                var currentStockTotal = 0.0;

                foreach (var foundStock in cryptoObject)
                {
                    currentStockTotal += foundStock.Amount;
                }

                if (crypto.Amount > currentStockTotal)
                {
                    fnResult.Message = $"{crypto.Name} sell amount is too high than current total.";
                    return fnResult;
                }

                if (crypto.Amount < cryptoObject.First().Amount)
                {
                    cryptoObject.First().Amount = cryptoObject.First().Amount - crypto.Amount;
                    _unitOfWork.Cryptocurrencies.Update(cryptoObject.First());
                    await _unitOfWork.Save();
                }
                else
                {
                    foreach (var currentCrypto in cryptoObject)
                    {
                        if (crypto.Amount >= currentCrypto.Amount)
                        {
                            crypto.Amount = crypto.Amount - currentCrypto.Amount;
                            await _unitOfWork.Stocks.Delete(currentCrypto.Id);
                        }
                        else
                        {
                            currentCrypto.Amount = currentCrypto.Amount - crypto.Amount;
                            _unitOfWork.Cryptocurrencies.Update(currentCrypto);
                        }
                    }

                    await _unitOfWork.Save();
                }

                fnResult.StatusCode = (int)HttpStatusCode.OK;
                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.Message = e.Message;
                return fnResult;
            }
        }
    }
}