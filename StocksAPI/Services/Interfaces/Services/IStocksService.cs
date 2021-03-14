using Services.Models.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface IStocksService
    {
        public Task<StockData> GetStockDataAsync(string id);

        public Task<PortfolioProfit> GetPortfolioProfitAsync();

        public Task<IEnumerable<CurrentStockProfile>> GetCurrentStockProfileAsync();
    }
}