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
                    var stockProfile = new StockValue
                    {
                        CurrentPrice = (double)requestBody.SelectToken($"quoteResponse.result[{count}].regularMarketPrice"),
                        Name = stock
                    };

                    stockProfiles.Add(stockProfile);
                    count++;
                }
            }

            return stockProfiles;
        }

        public async Task<StockPortfolio> GetPortfolio()
        {
            var stocks = await _unitOfWork.Stocks.GetAll();

            var stockPortfolio = new StockPortfolio();

            var ids = "";

            foreach (var stock in stocks)
            {
                var stockInfo = _mapper.Map<StockInfo>(stock);

                if (!stockPortfolio.CurrentStockPortfolio.ContainsKey(stockInfo.Name))
                {
                    stockPortfolio.CurrentStockPortfolio.Add(stockInfo.Name, new StockProfile
                    {
                        StockList = new List<StockInfo>
                        {
                            stockInfo
                        },
                        StockCount = 1,
                        AvgPrice = 0,
                        TotalProfit = 0,
                        TotalCost = 0,
                        CurrentValue = 0
                    });
                    ids += stockInfo.Name + ',';
                }
                else
                {
                    var profile = stockPortfolio.CurrentStockPortfolio[stockInfo.Name];
                    profile.StockList.Add(stockInfo);
                    profile.StockCount += 1;
                    stockPortfolio.CurrentStockPortfolio[stockInfo.Name] = profile;
                }
            }

            ids = ids.Remove(ids.Length - 1);

            var prices = await GetStockDataAsync(ids);

            foreach (var value in prices)
            {
                var stockProfile = stockPortfolio.CurrentStockPortfolio[value.Name];

                double avgPrice = 0;

                foreach (var entry in stockProfile.StockList)
                {
                    entry.CurrentPrice = value.CurrentPrice;

                    entry.CurrentValue = entry.Amount * entry.CurrentPrice;

                    stockProfile.CurrentValue += entry.CurrentValue;

                    entry.Profit = entry.CurrentValue - entry.TotalCost;

                    stockProfile.TotalProfit += entry.Profit;

                    stockProfile.TotalCost += entry.TotalCost;

                    avgPrice += entry.PurchasePrice;
                }

                stockProfile.AvgPrice = avgPrice / stockProfile.StockCount;

                stockPortfolio.PortfolioProfit.CurrentTotal += stockProfile.CurrentValue;
                stockPortfolio.PortfolioProfit.PurchaseTotal += stockProfile.TotalCost;
            }

            // Create portfolio profit

            return stockPortfolio;
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