using Services.Data;
using Services.Models;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repository
{
    public interface IStocksRepository
    {
        Task<Response<IEnumerable<StockValue>>> GetStockDataAsync(string id);

        Task<Response<StockSummaryData>> GetStockSummaryDataAsync(string id);

        Task<Response<List<StockNews>>> GetStockNewsAsync(string id);

        Task<Response<StockChartData>> GetStockChartDataAsync(string id);

        Task<Response<StockPortfolio>> GetStockPortfolioAsync();

        Task<StockDTO> AddStockDataAsync(StockDTO stock);

        Task<StockDTO> SellStockAsync(SellStockDTO stock);

        Task<PortfolioTrackerDTO> AddPortfolioValueAsync(PortfolioTrackerDTO portfolioTracker);

        Task<IEnumerable<PortfolioTrackerDTO>> GetPortfolioValueListAsync();
    }
}