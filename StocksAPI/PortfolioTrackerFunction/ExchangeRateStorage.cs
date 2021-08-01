using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace PortfolioTrackerFunction
{
    public static class ExchangeRateStorage
    {
        [FunctionName("ExchangeRateStorage")]
        public static void Run([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer, ILogger log, IBinder binder)
        {
            log.LogInformation($"ExchangeRateStorage timer function executed at: {DateTime.Now}");

            // TODO: ExchangeRate storage process
        }
    }
}