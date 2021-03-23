﻿using Services.Data;
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
        public Task<IEnumerable<CurrentStockProfile>> GetStockDataAsync(string id);

        public Task<Portfolio> GetPortfolio();

        public Task<StockDTO> AddStockDataAsync(StockDTO stock);
    }
}