using Services.Models.SoldInstruments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces.Repository
{
    public interface ISoldInstrumentsRepository
    {
        Task AddSoldInstrumentToDBTable(SoldInstrumentDTO soldInstrument);

        Task<Response<CGTOverview>> GetSoldInstrumentDataFromDBTable();
    }
}