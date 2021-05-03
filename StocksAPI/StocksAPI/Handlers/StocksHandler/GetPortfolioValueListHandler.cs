using MediatR;
using Services.Interfaces.Services;
using Services.Models;
using Services.Stocks.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.StocksHandler
{
    public class GetPortfolioValueListHandler : IRequestHandler<GetPortfolioValueListQuery, IEnumerable<PortfolioTrackerDTO>>
    {
        private readonly IStocksService _stocksService;

        public GetPortfolioValueListHandler(IStocksService stocksService)
        {
            _stocksService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        public Task<IEnumerable<PortfolioTrackerDTO>> Handle(GetPortfolioValueListQuery request, CancellationToken cancellationToken)
        {
            var result = _stocksService.GetPortfolioValueListAsync();

            return result;
        }
    }
}