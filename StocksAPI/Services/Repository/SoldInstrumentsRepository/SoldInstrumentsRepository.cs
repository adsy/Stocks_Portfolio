using AutoMapper;
using Services.Data;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models.SoldInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Services.Repository.SoldInstrumentsRepository
{
    public class SoldInstrumentsRepository : ISoldInstrumentsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly StockDbContext _dbContext;

        public SoldInstrumentsRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddSoldInstrumentToDBTable(SoldInstrumentDTO soldInstrument)
        {
            var soldInstrumentDBObject = _mapper.Map<SoldInstrument>(soldInstrument);

            await _unitOfWork.SoldInstruments.Insert(soldInstrumentDBObject);

            await _unitOfWork.Save();
        }

        public async Task<Response<CGTOverview>> GetSoldInstrumentDataFromDBTable()
        {
            var result = await _unitOfWork.SoldInstruments.GetAll();

            if (result == null)
                return new Response<CGTOverview>((int)HttpStatusCode.NotFound);

            var cgtOverview = new CGTOverview
            {
                SalesList = _mapper.Map<List<SoldInstrumentDTO>>(result)
            };

            cgtOverview.SalesList.ForEach(o =>
                {
                    cgtOverview.CGTPayable += o.CGTPayable;
                });

            return new Response<CGTOverview>
            {
                Data = cgtOverview,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}