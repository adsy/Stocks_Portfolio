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

namespace StockAPI.Handlers
{
    public class GetStockSummaryDataHandler : IRequestHandler<GetStockSummaryDataQuery, Response<StockSummaryData>>
    {
        private readonly IStocksService _stocksService;

        public GetStockSummaryDataHandler(IStocksService stocksService)
        {
            _stocksService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        public async Task<Response<StockSummaryData>> Handle(GetStockSummaryDataQuery request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.GetStockSummaryDataAsync(request.Id);

            return result;
        }
    }
}