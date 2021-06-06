using MediatR;
using Services;
using Services.Interfaces.Services;
using Services.Models.Stocks;
using Services.Stocks.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.StocksHandler
{
    public class GetStockPortfolioAsyncHandler : IRequestHandler<GetStockPortfolioQuery, Response<StockPortfolio>>
    {
        private readonly IStocksService _stockService;

        public GetStockPortfolioAsyncHandler(IStocksService stocksService)
        {
            _stockService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        public Task<Response<StockPortfolio>> Handle(GetStockPortfolioQuery request, CancellationToken cancellationToken)
        {
            var result = _stockService.GetPortfolioAsync();

            return result;
        }
    }
}