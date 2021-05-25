using MediatR;
using Services.Interfaces.Services;
using Services.IRepository;
using Services.Models.Stocks;
using Services.Stocks.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.StocksHandler
{
    public class GetPortfolioHandler : IRequestHandler<GetPortfolioQuery, StockPortfolio>
    {
        private readonly IStocksService _stocksService;

        public GetPortfolioHandler(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        public async Task<StockPortfolio> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.GetPortfolioAsync();

            return result;
        }
    }
}