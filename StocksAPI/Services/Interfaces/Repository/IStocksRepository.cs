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
        public Task<StockData> GetStockDataAsync(string id);

        public Task<PortfolioProfit> GetPortfolioProfitAsync();

        public Task<IEnumerable<CurrentStockProfile>> GetIndividualStockProfiles();
    }
}