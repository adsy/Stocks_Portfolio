using Microsoft.Azure.WebJobs;
using PortfolioTrackerFunction.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task<ServiceProcessResult<ExchangeRate>> GetExchangeRateFromTable(IBinder binder);
    }
}