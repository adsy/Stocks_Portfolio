using Services.Models.Portfolio;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repository
{
    public interface IPortfolioRepository
    {
        Task<CompletePortfolio> GetPortfolio();
    }
}