using MediatR;
using Services;
using Services.Cgt.Queries;
using Services.Interfaces.Services;
using Services.Models.SoldInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.CgtHandler
{
    public class GetCgtDataHandler : IRequestHandler<GetCgtDataQuery, Response<CGTOverview>>
    {
        private readonly ISoldInstrumentsService _soldInstrumentsService;

        public GetCgtDataHandler(ISoldInstrumentsService soldInstrumentsService)
        {
            _soldInstrumentsService = soldInstrumentsService ?? throw new ArgumentNullException(nameof(soldInstrumentsService));
        }

        public Task<Response<CGTOverview>> Handle(GetCgtDataQuery request, CancellationToken cancellationToken)
        {
            return _soldInstrumentsService.GetSoldInstrumentData();
        }
    }
}