﻿using Services.Interfaces.Repository;
using Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models.Stocks;

namespace Services.Services.GetStockDataService
{
    public class GetStockDataService : IStocksService
    {
        private readonly IStocksRepository _stocksRepository;

        public GetStockDataService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<IEnumerable<CurrentStockProfile>> GetStockDataAsync(string id)
        {
            var result = await _stocksRepository.GetStockDataAsync(id);

            return result;
        }

        public async Task<Portfolio> GetPortfolioAsync()
        {
            var result = await _stocksRepository.GetPortfolio();

            return result;
        }
    }
}