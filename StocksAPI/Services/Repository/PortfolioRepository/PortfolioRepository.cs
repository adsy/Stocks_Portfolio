using AutoMapper;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models.Portfolio;
using System;
using System.Threading.Tasks;

namespace Services.Repository.PortfolioRepository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IStocksRepository _stocksRepository;
        private readonly ICryptoRepository _cryptoRepository;

        public PortfolioRepository(IUnitOfWork unitOfWork, IMapper mapper, IStocksRepository stocksRepository, ICryptoRepository cryptoRepository)
        {
            _stocksRepository = stocksRepository ?? throw new ArgumentNullException(nameof(stocksRepository));
            _cryptoRepository = cryptoRepository ?? throw new ArgumentNullException(nameof(cryptoRepository));
        }

        public async Task<CompletePortfolio> GetPortfolio()
        {
            var fnResult = new CompletePortfolio();

            var stockResult = await _stocksRepository.GetStockPortfolioAsync();
            var cryptoResult = await _cryptoRepository.GetCryptoPortfolioAsync();

            fnResult.CurrentStockPortfolio = stockResult.Data;
            fnResult.CurrentCryptoPortfolio = cryptoResult.Data;

            // loop through and pull out current values and profit amounts for fnResult.PortfolioProfit property
            foreach (var stock in fnResult.CurrentStockPortfolio.Stocks)
            {
                fnResult.PortfolioProfit.CurrentTotal += stock.Value.CurrentValue;
                fnResult.PortfolioProfit.PurchaseTotal += stock.Value.TotalCost;
            }
            foreach (var crypto in fnResult.CurrentCryptoPortfolio.Cryptocurrencies)
            {
                fnResult.PortfolioProfit.CurrentTotal += crypto.Value.CurrentValue;
                fnResult.PortfolioProfit.PurchaseTotal += crypto.Value.TotalCost;
            }

            fnResult.PortfolioProfit.CurrentTotal = Math.Round(fnResult.PortfolioProfit.CurrentTotal, 2);
            fnResult.PortfolioProfit.PurchaseTotal = Math.Round(fnResult.PortfolioProfit.PurchaseTotal, 2);

            return fnResult;
        }
    }
}