using AutoMapper;
using Newtonsoft.Json.Linq;
using Services.Data;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models;
using Services.Models.Stocks;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Services.Models.SoldInstruments;
using Domain.Config;
using Microsoft.Extensions.Options;

namespace Services.Repository.GetStockDataRepository
{
    public class StockRepository : IStocksRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISoldInstrumentsRepository _soldInstrumentsRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly YahooFinanceApiSettings _yahooFinanceApiSettings;
        private readonly ExchangeRateStorageApiSettings _exchangeRateApiSettings;
        private readonly HttpClient client;

        public StockRepository(IUnitOfWork unitOfWork, IMapper mapper, ISoldInstrumentsRepository soldInstrumentsRepository, IHttpClientFactory httpClientFactory, IOptions<YahooFinanceApiSettings> yahooFinanceApiSettings, IOptions<ExchangeRateStorageApiSettings> exchangeRateApiSettings)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _soldInstrumentsRepository = soldInstrumentsRepository ?? throw new ArgumentNullException(nameof(soldInstrumentsRepository));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            _yahooFinanceApiSettings = yahooFinanceApiSettings.Value ?? throw new ArgumentNullException(nameof(yahooFinanceApiSettings));
            _exchangeRateApiSettings = exchangeRateApiSettings.Value ?? throw new ArgumentNullException(nameof(exchangeRateApiSettings));

            client = _httpClientFactory.CreateClient(_yahooFinanceApiSettings.ClientName);
            client.DefaultRequestHeaders.Add(_yahooFinanceApiSettings.GetApiHostHeader, _yahooFinanceApiSettings.GetApiHostHeaderValue);
            client.DefaultRequestHeaders.Add(_yahooFinanceApiSettings.GetApiKeyHeader, _yahooFinanceApiSettings.GetApiKeyHeaderValue);
        }

        #region ApiCalls

        public async Task<Response<IEnumerable<StockValue>>> GetStockDataAsync(string ids)
        {
            var fnResult = new Response<IEnumerable<StockValue>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var stockTickers = ids.Split(",");

                var uri = _yahooFinanceApiSettings.BaseClient + string.Format(_yahooFinanceApiSettings.GetQuotesUri, ids);

                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));

                if (response.StatusCode != HttpStatusCode.OK)
                    return fnResult;

                var content = await response.Content.ReadAsStringAsync();

                var stockProfiles = new List<StockValue>();

                var requestBody = JObject.Parse(content);

                var count = 0;

                foreach (var stock in stockTickers)
                {
                    if (stock != "")
                    {
                        if (requestBody.SelectToken($"quoteResponse.result[{count}].regularMarketPrice") != null)
                        {
                            var stockProfile = new StockValue
                            {
                                CurrentPrice = (double)requestBody.SelectToken($"quoteResponse.result[{count}].regularMarketPrice"),
                                Name = stock
                            };

                            stockProfiles.Add(stockProfile);
                        }
                        count++;
                    }
                }
                fnResult.Data = stockProfiles;
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

        public async Task<Response<StockSummaryData>> GetStockSummaryDataAsync(string id)
        {
            var fnResult = new Response<StockSummaryData>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            try
            {
                var headers = new Dictionary<string, string>
                {
                    { "x-rapidapi-key", "6cfe4c0de0mshe1d53492d5f62e3p169b6ajsn15168cece55a" },
                    { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" }
                };

                var response = await HttpRequest.SendGetCall($"https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-profile?symbol={id}&region=AU", headers);

                if (response.StatusCode != (int)HttpStatusCode.OK)
                    return fnResult;

                var requestBody = JObject.Parse(response.Data);

                double exchangeRate = 1;

                if (!id.Contains(".AX"))
                {
                    exchangeRate = 1.33;

                    var exRateResponse = await HttpRequest.SendGetCall($"https://portfoliotrackerfunction.azurewebsites.net/api/exchangerate");

                    if (exRateResponse.StatusCode == (int)HttpStatusCode.OK)
                    {
                        exchangeRate = double.Parse(exRateResponse.Data);
                    }
                }

                var stockSummary = new StockSummaryData
                {
                    FullName = (string)requestBody.SelectToken($"quoteType.longName"),
                    MarketCap = (string)requestBody.SelectToken($"price.marketCap.fmt"),
                    PercentChange = (double)requestBody.SelectToken($"price.regularMarketChangePercent.raw"),
                    YearHigh = (double)requestBody.SelectToken($"summaryDetail.fiftyTwoWeekHigh.raw") * exchangeRate,
                    YearLow = (double)requestBody.SelectToken($"summaryDetail.fiftyTwoWeekLow.raw") * exchangeRate,
                    DayHigh = (double)requestBody.SelectToken($"price.regularMarketDayHigh.raw") * exchangeRate,
                    DayLow = (double)requestBody.SelectToken($"price.regularMarketDayLow.raw") * exchangeRate,
                    OpenPrice = (double)requestBody.SelectToken($"price.regularMarketOpen.raw") * exchangeRate,
                };

                fnResult.Data = stockSummary;
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

        public async Task<Response<List<StockNews>>> GetStockNewsAsync(string id)
        {
            var fnResult = new Response<List<StockNews>>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var uri = _yahooFinanceApiSettings.BaseClient + string.Format(_yahooFinanceApiSettings.GetStockNewUri, id);

                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));

                if (response.StatusCode != HttpStatusCode.OK)
                    return fnResult;

                var content = await response.Content.ReadAsStringAsync();

                var responseBody = JObject.Parse(content);

                var newsList = new List<StockNews>();

                var tokenList = responseBody.SelectToken("items.result");

                foreach (var token in tokenList)
                {
                    var publishedDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    publishedDate = publishedDate.AddSeconds((long)token.SelectToken("published_at")).ToLocalTime();
                    var dateString = publishedDate.ToString("g");

                    newsList.Add(new StockNews
                    {
                        Title = (string)token.SelectToken("title"),
                        Link = (string)token.SelectToken("link"),
                        Summary = (string)token.SelectToken("summary"),
                        Publisher = (string)token.SelectToken("publisher"),
                        Type = (string)token.SelectToken("type"),
                        PublishedAt = dateString
                    });
                }

                fnResult.StatusCode = (int)HttpStatusCode.OK;
                fnResult.Data = newsList;

                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.Message = e.Message;
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                return fnResult;
            }
        }

        public async Task<Response<StockChartData>> GetStockChartDataAsync(string id)
        {
            var fnResult = new Response<StockChartData>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var uri = _yahooFinanceApiSettings.BaseClient + string.Format(_yahooFinanceApiSettings.GetStockChartUri, id);

                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));

                if (response.StatusCode != HttpStatusCode.OK)
                    return fnResult;

                var content = await response.Content.ReadAsStringAsync();

                var requestBody = JObject.Parse(content);

                var timestampList = requestBody.SelectToken($"chart.result[0].timestamp").Values<long>().ToList();
                var priceJToken = requestBody.SelectToken($"chart.result[0].indicators.quote[0].close");
                var priceList = new List<double>();

                foreach (var price in priceJToken)
                {
                    var priceString = price.ToString();
                    if (string.IsNullOrWhiteSpace(priceString))
                    {
                        priceList.Add(0);
                    }
                    else
                    {
                        priceList.Add((double)price);
                    }
                }

                var stockChartData = new StockChartData();

                for (int i = 0; i < timestampList.Count; i++)
                {
                    var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    start = start.AddSeconds(timestampList[i]).ToLocalTime();

                    stockChartData.ChartDataList.Add(new ChartData
                    {
                        price = priceList[i],
                        timestampLong = timestampList[i],
                        time = start.ToString("g")
                    });
                }

                fnResult.StatusCode = (int)HttpStatusCode.OK;
                fnResult.Data = stockChartData;

                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.Message = e.Message;
                return fnResult;
            }
        }

        public async Task<Response<StockPortfolio>> GetStockPortfolioAsync()
        {
            var fnResult = new Response<StockPortfolio>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var client = _httpClientFactory.CreateClient(_exchangeRateApiSettings.ClientName);

                var exRateResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, _exchangeRateApiSettings.BaseClient + _exchangeRateApiSettings.GetExchangeRateUri));

                // placeholder for when ExRateStorage Cache is not working
                var exchangeRate = 1.33;

                if (exRateResponse.StatusCode == HttpStatusCode.OK)
                {
                    var content = await exRateResponse.Content.ReadAsStringAsync();
                    exchangeRate = double.Parse(content);
                }

                var stocks = await _unitOfWork.Stocks.GetAll();

                var stockPortfolio = new StockPortfolio();

                var ids = "";

                foreach (var stock in stocks)
                {
                    var stockInfo = _mapper.Map<StockInfo>(stock);

                    if (!stockPortfolio.Stocks.ContainsKey(stockInfo.Name))
                    {
                        stockPortfolio.Stocks.Add(stockInfo.Name, new StockProfile
                        {
                            StockList = new List<StockInfo>
                        {
                            stockInfo
                        },
                            StockCount = 1
                        });
                        ids += stockInfo.Name + ',';
                    }
                    else
                    {
                        var profile = stockPortfolio.Stocks[stockInfo.Name];
                        profile.StockList.Add(stockInfo);
                        profile.StockCount += 1;
                        stockPortfolio.Stocks[stockInfo.Name] = profile;
                    }
                }

                ids = ids.Remove(ids.Length - 1);

                var prices = await GetStockDataAsync(ids);

                if (prices.StatusCode != (int)HttpStatusCode.OK)
                    throw new Exception("There was an exception thrown getting stock values while creating stock portfolio.");

                foreach (var value in prices.Data)
                {
                    var stockProfile = stockPortfolio.Stocks[value.Name];

                    stockProfile.StockName = value.Name;
                    double avgPrice = 0;

                    foreach (var entry in stockProfile.StockList)
                    {
                        if (!entry.Name.Contains(".AX"))
                        {
                            entry.TotalCost *= exchangeRate;
                            entry.PurchasePrice *= exchangeRate;
                            entry.CurrentPrice = value.CurrentPrice * exchangeRate;
                            entry.CurrentValue = entry.Amount * entry.CurrentPrice;
                            entry.Profit = entry.CurrentValue - entry.TotalCost;
                            stockProfile.CurrentPrice = value.CurrentPrice * exchangeRate;
                        }
                        else
                        {
                            entry.CurrentPrice = value.CurrentPrice;
                            entry.CurrentValue = entry.Amount * entry.CurrentPrice;
                            entry.Profit = entry.CurrentValue - entry.TotalCost;
                            stockProfile.CurrentPrice = value.CurrentPrice;
                        }

                        stockProfile.CurrentValue += entry.CurrentValue;

                        stockProfile.TotalProfit += entry.Profit;

                        stockProfile.TotalCost += entry.TotalCost;

                        stockProfile.TotalAmount += entry.Amount;

                        avgPrice += entry.PurchasePrice;
                    }

                    stockProfile.AvgPrice = avgPrice / stockProfile.StockCount;
                }

                // Create portfolio profit
                fnResult.Data = stockPortfolio;
                fnResult.StatusCode = (int)HttpStatusCode.OK;

                return fnResult;
            }
            catch (Exception e)
            {
                fnResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                fnResult.exception = e;
                return fnResult;
            }
        }

        #endregion ApiCalls

        #region DatabaseCalls

        public async Task<StockDTO> AddStockDataAsync(StockDTO stock)
        {
            var stockObj = _mapper.Map<Stock>(stock);

            if (stockObj.Country == "AU")
                stockObj.Name += ".AX";

            await _unitOfWork.Stocks.Insert(stockObj);

            await _unitOfWork.Save();

            //var fnResult = new Response { Data = stock, StatusCode = 200 };

            return stock;
        }

        public async Task<StockDTO> SellStockAsync(SellStockDTO stockBeingSold)
        {
            try
            {
                var stockObj = await _unitOfWork.Stocks.GetAll(q => q.Name == stockBeingSold.Name);
                var currentStockTotal = 0.0;

                foreach (var foundStock in stockObj)
                {
                    currentStockTotal += foundStock.Amount;
                }

                if (stockBeingSold.Amount > currentStockTotal)
                    return null;

                var returnStock = new StockDTO();

                var firstStockEntry = stockObj.First();

                if (stockBeingSold.Amount < firstStockEntry.Amount)
                {
                    firstStockEntry.Amount = firstStockEntry.Amount - stockBeingSold.Amount;

                    _unitOfWork.Stocks.Update(firstStockEntry);

                    await _unitOfWork.Save();

                    await InsertToSoldInstrumentTable(firstStockEntry, stockBeingSold);
                }
                else
                {
                    foreach (var currentStock in stockObj)
                    {
                        if (stockBeingSold.Amount >= currentStock.Amount)
                        {
                            var saleAmount = new SellStockDTO(stockBeingSold);
                            saleAmount.Amount = currentStock.Amount;

                            stockBeingSold.Amount = stockBeingSold.Amount - currentStock.Amount;
                            await _unitOfWork.Stocks.Delete(currentStock.Id);
                            await InsertToSoldInstrumentTable(currentStock, saleAmount);
                        }
                        else
                        {
                            await InsertToSoldInstrumentTable(currentStock, stockBeingSold);
                            currentStock.Amount = currentStock.Amount - stockBeingSold.Amount;

                            if (currentStock.Amount == 0)
                            {
                                await _unitOfWork.Stocks.Delete(currentStock.Id);
                            }
                            else
                            {
                                _unitOfWork.Stocks.Update(currentStock);
                            }

                            returnStock = _mapper.Map<StockDTO>(currentStock);
                        }
                    }

                    await _unitOfWork.Save();
                }

                return returnStock;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw ex;
            }
        }

        public async Task<PortfolioTrackerDTO> AddPortfolioValueAsync(PortfolioTrackerDTO portfolioTracker)
        {
            var portfolioObj = _mapper.Map<PortfolioTracker>(portfolioTracker);

            portfolioObj.TimeStamp = portfolioObj.TimeStamp.ToLocalTime();

            await _unitOfWork.PortfolioTrackers.Insert(portfolioObj);

            await _unitOfWork.Save();

            //var fnResult = new Response { Data = stock, StatusCode = 200 };

            return portfolioTracker;
        }

        public async Task<IEnumerable<PortfolioTrackerDTO>> GetPortfolioValueListAsync()
        {
            var result = ((List<PortfolioTracker>)await _unitOfWork.PortfolioTrackers.GetAll())
                .OrderByDescending(q => q.TimeStamp)
                .Take(200)
                .Reverse();

            var newList = new List<PortfolioTrackerDTO>();

            foreach (var entry in result)
            {
                var portfolioTrackerObj = _mapper.Map<PortfolioTrackerDTO>(entry);

                portfolioTrackerObj.timeString = portfolioTrackerObj.TimeStamp.ToString("g");

                newList.Add(portfolioTrackerObj);
            }

            return newList;
        }

        #endregion DatabaseCalls

        private async Task InsertToSoldInstrumentTable(Stock stockDbOBj, SellStockDTO stockSaleInfo)
        {
            var client = _httpClientFactory.CreateClient(_exchangeRateApiSettings.ClientName);

            var exRateResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, _exchangeRateApiSettings.BaseClient + _exchangeRateApiSettings.GetExchangeRateUri));

            // placeholder for when ExRateStorage Cache is not working
            var exchangeRate = 1.33;

            if (exRateResponse.StatusCode == HttpStatusCode.OK)
            {
                var content = await exRateResponse.Content.ReadAsStringAsync();
                exchangeRate = double.Parse(content);
            }

            if (stockDbOBj.Country == "US")
            {
                // create SoldInstrumentDTO with stockObj data and send to SoldInstrumentService
                var soldInstrumentDTO = new SoldInstrumentDTO
                {
                    Amount = stockSaleInfo.Amount,
                    Name = stockSaleInfo.Name,
                    PurchasePrice = stockDbOBj.PurchasePrice,
                    PuchaseDate = stockDbOBj.PurchaseDate,
                    SalePrice = stockSaleInfo.SellPrice,
                    SaleDate = DateTime.Now,
                    DiscountApplied = stockDbOBj.PurchaseDate.AddMonths(12) <= DateTime.Now ? true : false,
                    Profit = ((stockSaleInfo.SellPrice * exchangeRate) * stockSaleInfo.Amount) - ((stockDbOBj.PurchasePrice * exchangeRate) * stockSaleInfo.Amount)
                };

                soldInstrumentDTO.CGTPayable = soldInstrumentDTO.DiscountApplied ? (soldInstrumentDTO.Profit * .5) : soldInstrumentDTO.Profit;

                await _soldInstrumentsRepository.AddSoldInstrumentToDBTable(soldInstrumentDTO);
            }
            else
            {
                // create SoldInstrumentDTO with stockObj data and send to SoldInstrumentService
                var soldInstrumentDTO = new SoldInstrumentDTO
                {
                    Amount = stockSaleInfo.Amount,
                    Name = stockSaleInfo.Name,
                    PurchasePrice = stockDbOBj.PurchasePrice,
                    PuchaseDate = stockDbOBj.PurchaseDate,
                    SalePrice = stockSaleInfo.SellPrice,
                    SaleDate = DateTime.Now,
                    DiscountApplied = stockDbOBj.PurchaseDate.AddMonths(12) <= DateTime.Now ? true : false,
                    Profit = stockSaleInfo.SellPrice * stockSaleInfo.Amount - stockDbOBj.PurchasePrice * stockSaleInfo.Amount
                };

                soldInstrumentDTO.CGTPayable = !soldInstrumentDTO.DiscountApplied ? (soldInstrumentDTO.Profit * .5) : soldInstrumentDTO.Profit;

                await _soldInstrumentsRepository.AddSoldInstrumentToDBTable(soldInstrumentDTO);
            }
        }
    }
}