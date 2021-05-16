using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Models;

namespace PortfolioTrackerFunction
{
    public class PortfolioValueCheckerUS_Midnight
    {
        private readonly IStocksApiService _stockApiService;

        public PortfolioValueCheckerUS_Midnight(IStocksApiService stocksApiService)
        {
            _stockApiService = stocksApiService ?? throw new ArgumentNullException(nameof(stocksApiService));
        }

        [FunctionName("PortfolioValueCheckerUSAfterMidnight")]
        public async void Run([TimerTrigger("0 */5 1-5 * * 1-5")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await _stockApiService.AddPortfolioValueToDB();
            log.LogInformation($"Portfolio value added to DB at time: {DateTime.Now}");
        }
    }
}