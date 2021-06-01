using Services.Interfaces.Repository;
using Services.Interfaces.Services;
using Services.Models.Portfolio;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository ?? throw new ArgumentNullException(nameof(portfolioRepository));
        }

        public async Task<CompletePortfolio> GetPortfolio()
        {
            var result = await _portfolioRepository.GetPortfolio();

            return result;
        }
    }
}