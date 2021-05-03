using MediatR;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Stocks.Queries
{
    public class GetPortfolioValueListQuery : IRequest<IEnumerable<PortfolioTrackerDTO>>
    {
    }
}