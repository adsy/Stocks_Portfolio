using MediatR;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Stocks.Queries
{
    public class GetStockPortfolioQuery : IRequest<StockPortfolio>
    {
    }
}