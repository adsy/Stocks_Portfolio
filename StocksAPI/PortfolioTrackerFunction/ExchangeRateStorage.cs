using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using PortfolioTrackerFunction.Infrastructure.Interfaces;

namespace PortfolioTrackerFunction
{
    public class ExchangeRateStorage
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateStorage(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService ?? throw new ArgumentNullException(nameof(exchangeRateService));
        }

        [FunctionName("ExchangeRateStorage")]
        public async void Run([TimerTrigger("0 0 10 * * *")] TimerInfo myTimer, ILogger log, IBinder binder)
        {
            log.LogInformation($"ExchangeRateStorage timer function executed at: {DateTime.Now}");

            await _exchangeRateService.UpdateExchangeRate(binder);
        }

        [FunctionName("ExchangeRateStorageHttpTrigger")]
        public async void RunHttp([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "httptrigger")] HttpRequest req, ILogger logger, IBinder binder)
        {
            await _exchangeRateService.UpdateExchangeRate(binder);
        }
    }
}