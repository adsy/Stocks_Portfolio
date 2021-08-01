using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Interfaces
{
    public interface IStorageRepository
    {
        Task AddEntityToTable(IBinder binder, ITableEntity entity, string tableName);

        Task RemoveEntityFromTable(IBinder binder, string partitionKey, string rowKey, string tableName);

        Task<T> FetchEntityFromTable<T>(IBinder binder, string partitionKey, string rowKey, string tableName) where T : ITableEntity;
    }
}