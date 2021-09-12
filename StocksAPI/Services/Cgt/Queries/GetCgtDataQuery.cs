using MediatR;
using Services.Models.SoldInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Cgt.Queries
{
    public class GetCgtDataQuery : IRequest<Response<CGTOverview>>
    {
    }
}