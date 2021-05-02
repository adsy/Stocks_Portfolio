using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Services
{
    public class StocksApiService : IStocksApiService
    {
        private readonly ILogger<StocksApiService> _log;
        private readonly IStocksApiRepository _stocksApiRepository;

        public StocksApiService(ILogger<StocksApiService> log, IStocksApiRepository stocksApiRepository)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _stocksApiRepository = stocksApiRepository ?? throw new ArgumentNullException(nameof(stocksApiRepository));
        }

        public async Task<ServiceProcessResult> AddPortfolioValueToDB()
        {
            var fnResult = new ServiceProcessResult
            {
                ServiceResultCode = (int)HttpStatusCode.BadRequest
            };

            var portfolioValue = await _stocksApiRepository.GetPortfolioValue();

            var result = await _stocksApiRepository.AddValueToDB(portfolioValue.Data);

            fnResult.ServiceResultCode = result.ServiceResultCode;

            return fnResult;
        }
    }
}