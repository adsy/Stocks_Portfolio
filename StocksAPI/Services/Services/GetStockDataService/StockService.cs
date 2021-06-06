using Services.Interfaces.Repository;
using Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models.Stocks;
using Services.Data;
using Services.Models;

namespace Services.Services.GetStockDataService
{
    public class StockService : IStocksService
    {
        private readonly IStocksRepository _stocksRepository;

        public StockService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<IEnumerable<StockValue>> GetStockDataAsync(string id)
        {
            var result = await _stocksRepository.GetStockDataAsync(id);

            return result;
        }

        public async Task<Response<StockPortfolio>> GetPortfolioAsync()
        {
            var result = await _stocksRepository.GetStockPortfolioAsync();

            return result;
        }

        public async Task<StockDTO> AddStockAsync(StockDTO stock)
        {
            var result = await _stocksRepository.AddStockDataAsync(stock);

            return result;
        }

        public async Task<StockDTO> SellStockAsync(SellStockDTO stock)
        {
            var result = await _stocksRepository.SellStockAsync(stock);
            return result;
        }

        public async Task<PortfolioTrackerDTO> AddPortfolioValueAsync(PortfolioTrackerDTO portfolioTracker)
        {
            var result = await _stocksRepository.AddPortfolioValueAsync(portfolioTracker);

            return result;
        }

        public async Task<IEnumerable<PortfolioTrackerDTO>> GetPortfolioValueListAsync()
        {
            var result = await _stocksRepository.GetPortfolioValueListAsync();

            return result;
        }
    }
}