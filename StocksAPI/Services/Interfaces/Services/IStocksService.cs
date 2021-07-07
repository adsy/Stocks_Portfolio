using Services.Data;
using Services.Models;
using Services.Models.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface IStocksService
    {
        public Task<Response<IEnumerable<StockValue>>> GetStockDataAsync(string id);

        public Task<Response<StockSummaryData>> GetStockSummaryDataAsync(string id);

        public Task<Response<List<StockNews>>> GetStockNewsAsync(string id);

        public Task<Response<StockChartData>> GetStockChartDataAsync(string id);

        public Task<Response<StockPortfolio>> GetPortfolioAsync();

        public Task<StockDTO> AddStockAsync(StockDTO stock);

        public Task<StockDTO> SellStockAsync(SellStockDTO stock);

        public Task<PortfolioTrackerDTO> AddPortfolioValueAsync(PortfolioTrackerDTO portfolioTracker);

        public Task<IEnumerable<PortfolioTrackerDTO>> GetPortfolioValueListAsync();
    }
}