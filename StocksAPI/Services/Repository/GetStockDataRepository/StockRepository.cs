using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Data;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models;
using Services.Models.Stocks;
using System;
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

        public async Task<IEnumerable<CurrentStockProfile>> GetStockDataAsync(string ids)
        {
            var stockTickers = ids.Split(",");

            var headers = new Dictionary<string, string>
            {
                { "x-rapidapi-key", "6cfe4c0de0mshe1d53492d5f62e3p169b6ajsn15168cece55a" },
                { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" }
            };

            var response = await HttpRequest.SendGetCall($"https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-quotes?region=AU&symbols={ids}", headers);

            var stockProfiles = new List<CurrentStockProfile>();

            var requestBody = JObject.Parse(response);
            var count = 0;

            foreach (var stock in stockTickers)
            {
                if (stock != "")
                {
                    var stockProfile = new CurrentStockProfile
                    {
                        currentPrice = (double)requestBody.SelectToken($"quoteResponse.result[{count}].regularMarketPrice"),
                        Name = stock
                    };

                    stockProfiles.Add(stockProfile);
                    count++;
                }
            }

            return stockProfiles;
        }

        public async Task<Portfolio> GetPortfolio()
        {
            // Gets individual stock profiles and places the current prices into an array
            var body = await HttpRequest.SendGetCall("https://api.exchangeratesapi.io/latest?base=USD");
            ExchangeRate exchangeRate = JsonConvert.DeserializeObject<ExchangeRate>(body);

            var results = await _unitOfWork.Stocks.GetAll();

            results = EditData(results, exchangeRate);

            var ids = "";

            foreach (var stock in results)
            {
                if (!ids.Contains(stock.Name))
                {
                    ids += stock.Name + ",";
                }
            }

            var currentProfiles = await GetStockDataAsync(ids);

            var stockProfiles = CreateCurrentStockProfiles(results, (List<CurrentStockProfile>)currentProfiles, exchangeRate);

            // Create portfolio profit
            var portfolioProfit = GetPortfolioProfit(stockProfiles);

            var portfolio = new Portfolio { _PortfolioProfit = portfolioProfit, _CurrentStockPortfolio = stockProfiles };

            return portfolio;
        }

        #endregion ApiCalls

        #region DatabaseCalls

        public async Task<StockDTO> AddStockDataAsync(StockDTO stock)
        {
            var stockObj = _mapper.Map<Stock>(stock);

            await _unitOfWork.Stocks.Insert(stockObj);

            await _unitOfWork.Save();

            //var fnResult = new Response { Data = stock, StatusCode = 200 };

            return stock;
        }

        #endregion DatabaseCalls

        #region Helper Functions

        public List<Task<StockData>> AddTasksToList(List<Task<StockData>> taskList, IList<Stock> results, ExchangeRate exchangeRate)
        {
            foreach (var stock in results)
            {
                //taskList.Add(GetStockDataAsync(stock.Name));
            }

            return taskList;
        }

        public IList<Stock> EditData(IList<Stock> results, ExchangeRate exchangeRate)
        {
            foreach (var stock in results)
            {
                if (stock.Country == "AU")
                    stock.Name += ".AX";

                if (stock.Country == "US")
                    stock.TotalCost *= exchangeRate.Rates.AUD;
            }

            return results;
        }

        public List<CurrentStockProfile> CreateCurrentStockProfiles(IList<Stock> results, List<CurrentStockProfile> currentProfiles, ExchangeRate exchangeRate)
        {
            var count = 0;

            foreach (var stock in results)
            {
                var stockProfile = currentProfiles.Find(q => q.Name == stock.Name);

                stockProfile.Amount += stock.Amount;
                if (stockProfile.Country == null)
                {
                    stockProfile.Id = stock.Id;
                    // Needs to eventually be changed into average price
                    stockProfile.PurchasePrice = stock.PurchasePrice;
                    stockProfile.TotalCost += stock.TotalCost;
                    stockProfile.Country = stock.Country;
                }

                if (stock.Country == "AU")
                {
                    stockProfile.Country = "AU";
                    stockProfile.currentValue += stockProfile.currentPrice * stock.Amount;
                    stockProfile.profit += stockProfile.currentPrice * stock.Amount - stock.TotalCost;
                    count++;
                    continue;
                }

                stockProfile.Country = "US";
                stockProfile.currentValue += (stockProfile.currentPrice * exchangeRate.Rates.AUD) * stock.Amount;
                stockProfile.profit += (stockProfile.currentPrice * stock.Amount * exchangeRate.Rates.AUD) - stock.TotalCost;
                count++;
                continue;

                // implement functionality if multiple purchases exists for average price
            }

            return currentProfiles;
        }

        public PortfolioProfit GetPortfolioProfit(List<CurrentStockProfile> currentPortfolio)
        {
            var portfolioProfit = new PortfolioProfit();

            foreach (var stock in currentPortfolio)
            {
                portfolioProfit.PurchaseTotal += stock.TotalCost;
                portfolioProfit.CurrentTotal += stock.currentValue;
            }

            return portfolioProfit;
        }

        #endregion Helper Functions
    }
}