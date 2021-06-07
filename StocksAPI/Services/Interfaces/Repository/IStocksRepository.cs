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
        public Task<IEnumerable<StockValue>> GetStockDataAsync(string id);

        public Task<Response<StockChartData>> GetStockChartDataAsync(string id);

        Task<Response<StockPortfolio>> GetStockPortfolioAsync();

        public Task<StockDTO> AddStockDataAsync(StockDTO stock);

        public Task<StockDTO> SellStockAsync(SellStockDTO stock);

        public Task<PortfolioTrackerDTO> AddPortfolioValueAsync(PortfolioTrackerDTO portfolioTracker);

        public Task<IEnumerable<PortfolioTrackerDTO>> GetPortfolioValueListAsync();
    }
}