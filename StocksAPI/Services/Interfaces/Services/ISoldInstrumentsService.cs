using Services.Models.SoldInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface ISoldInstrumentsService
    {
        Task<Response> AddToSoldInstrumentsTable(SoldInstrumentDTO soldInstrument);

        Task<Response<CGTOverview>> GetSoldInstrumentData();
    }
}