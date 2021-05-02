using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Interfaces
{
    public interface IStocksApiRepository
    {
        Task<ServiceProcessResult<double>> GetPortfolioValue();

        Task<ServiceProcessResult> AddValueToDB(double value);
    }
}