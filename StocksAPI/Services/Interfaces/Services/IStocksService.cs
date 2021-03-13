using Services.Models.Stocks;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface IStocksService
    {
        public Task<StockData> GetStockDataAsync(string id);

        public Task<PortfolioProfit> GetPortfolioProfitAsync();
    }
}