using Microsoft.Azure.WebJobs;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public ExchangeRateService(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository ?? throw new ArgumentNullException(nameof(exchangeRateRepository));
        }

        public async Task<ServiceProcessResult<ExchangeRate>> GetExchangeRate(IBinder binder)
        {
            var result = await _exchangeRateRepository.GetExchangeRateFromTable(binder);

            return result;
        }

        public async Task UpdateExchangeRate(IBinder binder)
        {
            await _exchangeRateRepository.UpdateExchangeRateInTable(binder);
        }
    }
}