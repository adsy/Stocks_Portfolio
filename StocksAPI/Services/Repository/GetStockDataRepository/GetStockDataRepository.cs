using Newtonsoft.Json;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models;
using Services.Models.Stocks;
using System;
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

        public async Task<PortfolioProfit> GetPortfolioProfitAsync()
        {
            var exchangeRate = new ExchangeRate();

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.exchangeratesapi.io/latest?base=USD")
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                exchangeRate = JsonConvert.DeserializeObject<ExchangeRate>(body);
            }

            var results = await _unitOfWork.Stocks.GetAll();
            var portfolioProfit = new PortfolioProfit();

            foreach (var stock in results)
            {
                if (stock.Country == "US")
                    stock.TotalCost *= exchangeRate.Rates.AUD;

                portfolioProfit.PurchaseTotal += stock.TotalCost;

                if (stock.Country == "AU")
                    stock.Name += ".AX";

                var currentStockPrice = await GetStockDataAsync(stock.Name);

                if (stock.Country == "US")
                {
                    portfolioProfit.CurrentTotal += (currentStockPrice.price.regularMarketOpen.raw * exchangeRate.Rates.AUD) * stock.Amount;
                    continue;
                }
                portfolioProfit.CurrentTotal += currentStockPrice.price.regularMarketOpen.raw * stock.Amount;
            }

            return portfolioProfit;
        }

        public async Task<StockData> GetStockDataAsync(string id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://apidojo-yahoo-finance-v1.p.rapidapi.com/stock/v2/get-summary?symbol={id}&region=AU"),
                Headers =
                    {
                        { "x-rapidapi-key", "6cfe4c0de0mshe1d53492d5f62e3p169b6ajsn15168cece55a" },
                        { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" },
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<StockData>(body);
                return result;
            }
        }
    }
}