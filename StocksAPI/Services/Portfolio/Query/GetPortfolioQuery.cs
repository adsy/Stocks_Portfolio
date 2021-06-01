using MediatR;
using Services.Models.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Portfolio.Query
{
    public class GetPortfolioQuery : IRequest<CompletePortfolio>
    {
    }
}