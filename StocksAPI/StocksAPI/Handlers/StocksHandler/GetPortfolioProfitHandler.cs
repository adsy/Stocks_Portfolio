using MediatR;
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
    public class GetPortfolioProfitHandler : IRequestHandler<GetPortfolioProfitQuery, PortfolioProfit>
    {
        private readonly IStocksService _stocksService;

        public GetPortfolioProfitHandler(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        public async Task<PortfolioProfit> Handle(GetPortfolioProfitQuery request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.GetPortfolioProfitAsync();

            return result;
        }
    }
}