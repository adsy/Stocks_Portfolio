using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Cosmos.Table;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Models;
using System.Net;
using System.Threading.Tasks;
using System;

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
    }
}