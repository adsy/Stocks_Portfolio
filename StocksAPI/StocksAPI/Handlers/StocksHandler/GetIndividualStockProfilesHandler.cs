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
    public class GetIndividualStockProfilesHandler : IRequestHandler<GetIndividualStockProfilesQuery, IEnumerable<CurrentStockProfile>>
    {
        private readonly IStocksService _stocksService;

        public GetIndividualStockProfilesHandler(IStocksService stocksService)
        {
            _stocksService = stocksService;
        }

        public async Task<IEnumerable<CurrentStockProfile>> Handle(GetIndividualStockProfilesQuery request, CancellationToken cancellationToken)
        {
            var result = await _stocksService.GetCurrentStockProfileAsync();

            return result;
        }
    }
}