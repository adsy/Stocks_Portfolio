using Services.Interfaces.Repository;
using Services.Interfaces.Services;
using Services.Models.SoldInstruments;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Services.Services.SoldInstrumentsService
{
    public class SoldInstrumentsService : ISoldInstrumentsService
    {
        private readonly ISoldInstrumentsRepository _soldInstrumentsRepository;

        public SoldInstrumentsService(ISoldInstrumentsRepository soldInstrumentsRepository)
        {
            _soldInstrumentsRepository = soldInstrumentsRepository ?? throw new ArgumentNullException(nameof(soldInstrumentsRepository));
        }

        public async Task<Response> AddToSoldInstrumentsTable(SoldInstrumentDTO soldInstrument)
        {
            if (soldInstrument == null)
                return new Response<Task>((int)HttpStatusCode.BadRequest);

            await _soldInstrumentsRepository.AddSoldInstrumentToDBTable(soldInstrument);

            return new Response<Task>((int)HttpStatusCode.OK);
        }

        public async Task<Response<CGTOverview>> GetSoldInstrumentData()
        {
            var result = await _soldInstrumentsRepository.GetSoldInstrumentDataFromDBTable();

            return result;
        }
    }
}