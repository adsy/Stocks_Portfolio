using MediatR;
using Services;
using Services.Interfaces.Services;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.StocksHandler
{
    public class GetStockChartDataHandler : IRequestHandler<GetStockChartDataQuery, Response<StockChartData>>
    {
        private readonly IStocksService _stocksService;

        public GetStockChartDataHandler(IStocksService stocksService)
        {
            _stocksService = stocksService ?? throw new ArgumentNullException(nameof(stocksService));
        }

        public async Task<Response<StockChartData>> Handle(GetStockChartDataQuery request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.GetStockChartDataAsync(request.Id);

            return result;
        }
    }
}