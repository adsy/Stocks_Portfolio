using AutoMapper;
using Newtonsoft.Json;
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

namespace Services.Repository.GetStockDataRepository
{
    public class StockRepository : IStocksRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region ApiCalls

        public async Task<IEnumerable<StockValue>> GetStockDataAsync(string ids)
        {
            var stockTickers = ids.Split(",");

            var headers = new Dictionary<string, string>
            {
                { "x-rapidapi-key", "6cfe4c0de0mshe1d53492d5f62e3p169b6ajsn15168cece55a" },
                { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" }
            };

            var response = await HttpRequest.SendGetCall($"https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-quotes?region=AU&symbols={ids}", headers);

            var stockProfiles = new List<StockValue>();

            var requestBody = JObject.Parse(response);
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

            return stockProfiles;
        }

        public async Task<Response<StockPortfolio>> GetStockPortfolioAsync()
        {
            var fnResult = new Response<StockPortfolio>
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                var response = await HttpRequest.SendGetCall($"https://v6.exchangerate-api.com/v6/23871810682eac22320017d5/latest/USD");

                var requestBody = JObject.Parse(response);

                var exchangeRate = (double)requestBody.SelectToken($"conversion_rates.AUD");

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

                foreach (var value in prices)
                {
                    var stockProfile = stockPortfolio.Stocks[value.Name];

                    stockProfile.StockName = value.Name;

                    stockProfile.CurrentPrice = value.CurrentPrice;

                    double avgPrice = 0;

                    foreach (var entry in stockProfile.StockList)
                    {
                        if (!entry.Name.Contains(".AX"))
                        {
                            entry.TotalCost *= exchangeRate;
                            entry.CurrentPrice = value.CurrentPrice * exchangeRate;
                            entry.CurrentValue = entry.Amount * entry.CurrentPrice;
                            entry.Profit = entry.CurrentValue - entry.TotalCost;
                        }
                        else
                        {
                            entry.CurrentPrice = value.CurrentPrice;
                            entry.CurrentValue = entry.Amount * entry.CurrentPrice;
                            entry.Profit = entry.CurrentValue - entry.TotalCost;
                        }
                        //entry.CurrentPrice = value.CurrentPrice;
                        //entry.CurrentValue = entry.Amount * entry.CurrentPrice;
                        //entry.Profit = entry.CurrentValue - entry.TotalCost;

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

        public async Task<StockDTO> SellStockAsync(SellStockDTO stock)
        {
            var stockObj = await _unitOfWork.Stocks.GetAll(q => q.Name == stock.Name);
            var currentStockTotal = 0.0;

            foreach (var foundStock in stockObj)
            {
                currentStockTotal += foundStock.Amount;
            }

            if (stock.Amount > currentStockTotal)
                return null;

            var returnStock = new StockDTO();

            if (stock.Amount < stockObj.First().Amount)
            {
                stockObj.First().Amount = stockObj.First().Amount - stock.Amount;
                _unitOfWork.Stocks.Update(stockObj.First());
                await _unitOfWork.Save();
            }
            else
            {
                foreach (var currentStock in stockObj)
                {
                    if (stock.Amount >= currentStock.Amount)
                    {
                        stock.Amount = stock.Amount - currentStock.Amount;
                        await _unitOfWork.Stocks.Delete(currentStock.Id);
                    }
                    else
                    {
                        currentStock.Amount = currentStock.Amount - stock.Amount;
                        _unitOfWork.Stocks.Update(currentStock);
                        returnStock = _mapper.Map<StockDTO>(currentStock);
                    }
                }

                await _unitOfWork.Save();
            }

            return returnStock;
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
            var result = await _unitOfWork.PortfolioTrackers.GetAll(q => q.TimeStamp > DateTime.Now.AddDays(-1));

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
    }
}