using Services.Data;
using Services.Models;
using Services.Models.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface IStocksService
    {
        Task<Response<IEnumerable<StockValue>>> GetStockDataAsync(string id);

        Task<Response<StockSummaryData>> GetStockSummaryDataAsync(string id);

        Task<Response<List<StockNews>>> GetStockNewsAsync(string id);

        Task<Response<StockChartData>> GetStockChartDataAsync(string id);

        Task<Response<StockPortfolio>> GetPortfolioAsync();

        Task<StockDTO> AddStockAsync(StockDTO stock);

        Task<StockDTO> SellStockAsync(SellStockDTO stock);

        Task<PortfolioTrackerDTO> AddPortfolioValueAsync(PortfolioTrackerDTO portfolioTracker);

        Task<IEnumerable<PortfolioTrackerDTO>> GetPortfolioValueListAsync();
    }
}