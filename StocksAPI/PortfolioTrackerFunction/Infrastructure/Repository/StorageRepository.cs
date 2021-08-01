using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Models;
using System;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Repository
{
    public class StorageRepository : IStorageRepository
    {
        public async Task AddEntityToTable(IBinder binder, ITableEntity entity, string tableName)
        {
            var table = await FetchTable(binder, tableName);

            var insertOperation = TableOperation.InsertOrReplace(entity);

            await table.ExecuteAsync(insertOperation);
        }

        private async Task<CloudTable> CreateTableIfNotExist(IBinder binder, string tableName)
        {
            var attribute = new TableAttribute(tableName);

            var table = await binder.BindAsync<CloudTable>(attribute);

            await table.CreateIfNotExistsAsync();

            return table;
        }

        private async Task<CloudTable> FetchTable(IBinder binder, string tableName)
        {
            return await CreateTableIfNotExist(binder, tableName);
        }

        public async Task RemoveEntityFromTable(IBinder binder, string partitionKey, string rowKey, string tableName)
        {
            var table = await FetchTable(binder, tableName);

            var deleteOperation = TableOperation.Delete(new DynamicTableEntity
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,
                ETag = "*"
            });

            var result = await table.ExecuteAsync(deleteOperation);
        }

        public async Task<T> FetchEntityFromTable<T>(IBinder binder, string partitionKey, string rowKey, string tableName) where T : ITableEntity
        {
            var table = await FetchTable(binder, tableName);

            var fetchOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            var result = await table.ExecuteAsync(fetchOperation);

            return (T)result.Result;
        }
    }
}