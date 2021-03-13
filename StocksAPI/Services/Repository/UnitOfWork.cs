using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.IRepository;
using Services.Data;

namespace Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockDbContext _dbContext;
        private IGenericRepository<Stock> _stocks;

        public UnitOfWork(StockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Stock> Stocks => _stocks ?? new GenericRepository<Stock>(_dbContext);

        public void Dispose()
        {
            // Kill any memory related to db connection was using
            _dbContext.Dispose();

            // GC - Garbage Collector
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}