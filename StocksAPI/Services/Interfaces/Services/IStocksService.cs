using Services.Models.Stocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface IStocksService
    {
        public Task<IEnumerable<CurrentStockProfile>> GetStockDataAsync(string id);

        public Task<Portfolio> GetPortfolioAsync();
    }
}