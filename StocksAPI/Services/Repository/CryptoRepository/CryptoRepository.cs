using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.CryptoRepository
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CryptoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                var headers = new Dictionary<string, string>
                    {
                        { "x-rapidapi-key", "6cfe4c0de0mshe1d53492d5f62e3p169b6ajsn15168cece55a" },
                        { "x-rapidapi-host", "coingecko.p.rapidapi.com" }
                    };

                var apiUrl = $"https://coingecko.p.rapidapi.com/coins/{id}/market_chart?vs_currency=aud&days=1";

                var response = await HttpRequest.SendGetCall(apiUrl, headers);

                var chartResponse = JsonConvert.DeserializeObject<CryptoChartResponse>(response);

                var chartData = chartResponse.Prices.Select(q => new ChartData
                {
                    timestampLong = Convert.ToInt64(q[0]),
                    price = Convert.ToDouble(q[1])
                }).ToList();

                foreach (var data in chartData)
                {
                    var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    start = start.AddSeconds(data.timestampLong/1000).ToLocalTime();
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

        public async Task<CryptoPortfolio> GetCryptoPortfolioAsync()
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

            foreach (var value in prices)
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

                    avgPrice += entry.PurchasePrice;
                }

                coinProfile.TotalCost = Math.Round(coinProfile.TotalCost, 2);
                coinProfile.TotalProfit = Math.Round(coinProfile.TotalProfit, 2);
                coinProfile.CurrentValue = Math.Round(coinProfile.CurrentValue, 2);
                coinProfile.CurrentPrice = Math.Round(coinProfile.CurrentPrice, 5);
                coinProfile.TotalAmount = Math.Round(coinProfile.TotalAmount, 3);

                coinProfile.AvgPrice = Math.Round(avgPrice / coinProfile.CoinCount, 3);
            }

            return cryptoPortfolio;
        }

        public async Task<IEnumerable<CryptoValue>> GetCryptoValuesAsync(string ids)
        {
            var cryptoTickers = ids.Split(",");

            var apiUrl = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol={ids}&convert=AUD";

            var headers = new Dictionary<string, string>
            {
                { "X-CMC_PRO_API_KEY", "a9c6f35a-153a-4123-8709-7782164e2e2b" }
            };

            var response = await HttpRequest.SendGetCall(apiUrl, headers);

            var requestBody = JObject.Parse(response);

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

            return cryptoValueList;
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