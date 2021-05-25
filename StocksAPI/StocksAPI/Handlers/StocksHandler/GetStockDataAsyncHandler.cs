using MediatR;
using Services.Models;
using Services.Interfaces.Services;
using Services.Stocks.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Stocks;

namespace StockAPI.Handlers.StocksHandler
{
    public class GetStockDataAsyncHandler : IRequestHandler<GetStockDataQuery, IEnumerable<StockValue>>
    {
        private readonly IStocksService _stockService;

        public GetStockDataAsyncHandler(IStocksService stockService)
        {
            _stockService = stockService;
        }

        public Task<IEnumerable<StockValue>> Handle(GetStockDataQuery request, CancellationToken cancellationToken)
        {
            var result = _stockService.GetStockDataAsync(request.Id);

            return result;
        }
    }
}