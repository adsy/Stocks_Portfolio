using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Cosmos.Table;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Models;
using System.Net;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json.Linq;
using PortfolioTrackerFunction.Models;

namespace PortfolioTrackerFunction.Infrastructure.Repository
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IStorageRepository _storageRepository;

        public ExchangeRateRepository(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository ?? throw new ArgumentNullException(nameof(storageRepository));
        }

        public async Task<ServiceProcessResult<ExchangeRate>> GetExchangeRateFromTable(IBinder binder)
        {
            var fnResult = new ServiceProcessResult<ExchangeRate>
            {
                ServiceResultCode = (int)HttpStatusCode.OK
            };

            try
            {
                // TODO: hardcode exchange-rate string name
                var result = await _storageRepository.FetchEntityFromTable<ExchangeRate>(binder, "exchange-rate", "rate", "exchangeRate");

                if (result == null)
                {
                    fnResult.ServiceResultCode = (int)HttpStatusCode.NotFound;
                    fnResult.ServiceResultMessage = "No cached exchangeRate found";
                    return fnResult;
                }

                fnResult.Data = result;
                return fnResult;
            }
            catch (Exception ex)
            {
                fnResult.ServiceResultCode = (int)HttpStatusCode.InternalServerError;
                fnResult.ServiceResultMessage = ex.Message;
                return fnResult;
            }
        }

        public async Task UpdateExchangeRateInTable(IBinder binder)
        {
            var exRateObj = new ExchangeRate
            {
                PartitionKey = "exchange-rate",
                RowKey = "rate"
            };

            var exRateRepsonse = await HttpRequest.SendGetCall($"https://portfoliotrackerfunction.azurewebsites.net/api/exchangerate");

            if (exRateRepsonse.StatusCode == (int)HttpStatusCode.OK)
            {
                var exRateBody = JObject.Parse(exRateRepsonse.Data);

                if (!(exRateBody.SelectToken("result").ToString() == "error"))
                {
                    var exchangeRate = (double)exRateBody.SelectToken($"conversion_rates.AUD");

                    exRateObj.rate = exchangeRate;

                    // check for existing item in table and remove it
                    await _storageRepository.RemoveEntityFromTable(binder, "exchange-rate", "rate", "exchangeRate");

                    await _storageRepository.AddEntityToTable(binder, exRateObj, "exchangeRate");
                }
            }
        }
    }
}