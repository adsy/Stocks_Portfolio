using Newtonsoft.Json;
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
    public class GetStockDataRepository : IStocksRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStockDataRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ApiCalls

        public async Task<PortfolioProfit> GetPortfolioProfitAsync()
        {
            var exchangeRate = new ExchangeRate();
            var body = await HttpRequest.SendGetCall("https://api.exchangeratesapi.io/latest?base=USD");
            exchangeRate = JsonConvert.DeserializeObject<ExchangeRate>(body);

            var results = await _unitOfWork.Stocks.GetAll();

            var portfolioProfit = new PortfolioProfit();

            var stockApiCalls = new List<Task<StockData>>();

            foreach (var stock in results)
            {
                if (stock.Country == "AU")
                    stock.Name += ".AX";
                stockApiCalls.Add(GetStockDataAsync(stock.Name));
            }

            var currentPrices = await Task.WhenAll(stockApiCalls);
            var count = 0;

            foreach (var stock in results)
            {
                if (stock.Country == "US")
                    stock.TotalCost *= exchangeRate.Rates.AUD;

                portfolioProfit.PurchaseTotal += stock.TotalCost;

                if (stock.Country == "US")
                {
                    portfolioProfit.CurrentTotal += (currentPrices[count].price.regularMarketOpen.raw * exchangeRate.Rates.AUD) * stock.Amount;
                    count++;
                    continue;
                }
                portfolioProfit.CurrentTotal += currentPrices[count].price.regularMarketOpen.raw * stock.Amount;
                count++;
            }

            return portfolioProfit;
        }

        public async Task<StockData> GetStockDataAsync(string id)
        {
            var headers = new Dictionary<string, string>
            {
                { "x-rapidapi-key", "6cfe4c0de0mshe1d53492d5f62e3p169b6ajsn15168cece55a" },
                { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" }
            };

            var response = await HttpRequest.SendGetCall($"https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-summary?symbol={id}&region=AU", headers);

            var result = JsonConvert.DeserializeObject<StockData>(response);

            return result;
        }

        public async Task<IEnumerable<CurrentStockProfile>> GetIndividualStockProfiles()
        {
            var body = await HttpRequest.SendGetCall("https://api.exchangeratesapi.io/latest?base=USD");
            ExchangeRate exchangeRate = JsonConvert.DeserializeObject<ExchangeRate>(body);

            var results = await _unitOfWork.Stocks.GetAll();

            var stockApiCalls = new List<Task<StockData>>();

            results = EditData(results, exchangeRate);

            stockApiCalls = AddTasksToList(stockApiCalls, results, exchangeRate);

            var currentPrices = await Task.WhenAll(stockApiCalls);

            var stockProfiles = CreateCurrentStockProfiles(results, currentPrices, exchangeRate);

            return stockProfiles;
        }

        #endregion ApiCalls

        #region Helper Functions

        public List<Task<StockData>> AddTasksToList(List<Task<StockData>> taskList, IList<Stock> results, ExchangeRate exchangeRate)
        {
            foreach (var stock in results)
            {
                taskList.Add(GetStockDataAsync(stock.Name));
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

        public List<CurrentStockProfile> CreateCurrentStockProfiles(IList<Stock> results, StockData[] currentPrices, ExchangeRate exchangeRate)
        {
            var count = 0;

            var stockProfiles = new List<CurrentStockProfile>();

            foreach (var stock in results)
            {
                if (stock.Country == "AU")
                {
                    stockProfiles.Add(new CurrentStockProfile(stock)
                    {
                        currentPrice = currentPrices[count].price.regularMarketOpen.raw,
                        currentValue = currentPrices[count].price.regularMarketOpen.raw * stock.Amount,
                        profit = currentPrices[count].price.regularMarketOpen.raw * stock.Amount - stock.TotalCost
                    });
                    count++;
                    continue;
                }

                stockProfiles.Add(new CurrentStockProfile(stock)
                {
                    currentPrice = currentPrices[count].price.regularMarketOpen.raw,
                    currentValue = (currentPrices[count].price.regularMarketOpen.raw * exchangeRate.Rates.AUD) * stock.Amount,
                    profit = (currentPrices[count].price.regularMarketOpen.raw * exchangeRate.Rates.AUD) * stock.Amount - stock.TotalCost
                });
                count++;
            }

            return stockProfiles;
        }

        #endregion Helper Functions
    }
}