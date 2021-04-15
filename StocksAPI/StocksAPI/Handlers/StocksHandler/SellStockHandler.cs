using MediatR;
using Services.Interfaces.Services;
using Services.Models;
using Services.Stocks.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.StocksHandler
{
    public class SellStockHandler : IRequestHandler<SellStockCommand, StockDTO>
    {
        private readonly IStocksService _stocksService;

        public SellStockHandler(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        public async Task<StockDTO> Handle(SellStockCommand request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.SellStockAsync(request.sellStockDTO);
            return result;
        }
    }
}