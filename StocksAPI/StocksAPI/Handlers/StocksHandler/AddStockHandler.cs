using AutoMapper;
using MediatR;
using Services.Data;
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
    public class AddStockHandler : IRequestHandler<AddStockCommand, StockDTO>
    {
        private readonly IStocksService _stockService;

        public AddStockHandler(IStocksService stockService)
        {
            _stockService = stockService;
        }

        public Task<StockDTO> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var result = _stockService.AddStockAsync(request.stock);

            return result;
        }
    }
}