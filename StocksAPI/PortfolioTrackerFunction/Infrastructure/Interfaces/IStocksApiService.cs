using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Interfaces
{
    public interface IStocksApiService
    {
        Task<ServiceProcessResult> AddPortfolioValueToDB();
    }
}