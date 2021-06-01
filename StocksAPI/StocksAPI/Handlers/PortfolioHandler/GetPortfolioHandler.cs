using MediatR;
using Services.Interfaces.Services;
using Services.IRepository;
using Services.Models.Portfolio;
using Services.Models.Stocks;
using Services.Portfolio.Query;
using Services.Stocks.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.PortfolioHandler
{
    public class GetPortfolioHandler : IRequestHandler<GetPortfolioQuery, CompletePortfolio>
    {
        private readonly IPortfolioService _portfolioService;

        public GetPortfolioHandler(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService ?? throw new ArgumentNullException(nameof(portfolioService));
        }

        public async Task<CompletePortfolio> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
        {
            var result = await _portfolioService.GetPortfolio();

            return result;
        }
    }
}