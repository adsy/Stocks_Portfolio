using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using System.Net;

namespace PortfolioTrackerFunction
{
    public class ExchangeRateRetrieval
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateRetrieval(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService ?? throw new ArgumentNullException(nameof(exchangeRateService));
        }

        [FunctionName("ExchangeRateRetrieval")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "exchangerate")] HttpRequest req,
            ILogger log, IBinder binder)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _exchangeRateService.GetExchangeRate(binder);

            if (result.ServiceResultCode == (int)HttpStatusCode.OK)
            {
                return new OkObjectResult(result.Data.rate);
            }

            return new StatusCodeResult(result.ServiceResultCode);
        }
    }
}