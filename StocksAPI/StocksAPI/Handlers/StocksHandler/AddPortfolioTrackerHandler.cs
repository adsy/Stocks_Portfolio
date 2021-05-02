using MediatR;
using Services.Interfaces.Services;
using Services.Models;
using Services.Stocks.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.StocksHandler
{
    public class AddPortfolioTrackerHandler : IRequestHandler<AddPortfolioValueCommand, PortfolioTrackerDTO>
    {
        private readonly IStocksService _stocksService;

        public AddPortfolioTrackerHandler(IStocksService stocksService)
        {
            _stocksService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        public async Task<PortfolioTrackerDTO> Handle(AddPortfolioValueCommand request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.AddPortfolioValueAsync(request.portfolioTracker);

            return result;
        }
    }
}