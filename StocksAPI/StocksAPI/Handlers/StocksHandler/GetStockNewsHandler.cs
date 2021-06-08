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
    public class GetStockNewsHandler : IRequestHandler<GetStockNewsQuery, Response<List<StockNews>>>
    {
        private readonly IStocksService _stocksService;

        public GetStockNewsHandler(IStocksService stocksService)
        {
            _stocksService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        public async Task<Response<List<StockNews>>> Handle(GetStockNewsQuery request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.GetStockNewsAsync(request.Id);

            return result;
        }
    }
}