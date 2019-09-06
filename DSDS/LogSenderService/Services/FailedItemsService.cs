using System.Collections.Generic;
using System.Threading.Tasks;
using LogSenderService.Models;
using MongoDB.Driver;

namespace LogSenderService.Services
{
    public class FailedItemsService
    {
        private readonly IMongoCollection<FailedItem> _failedItems;

        public FailedItemsService(IDatabaseSettings settings)
        {
            var client  = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _failedItems = database.GetCollection<FailedItem>(settings.CollectionName);
        }

        public async Task LogSingleItem(FailedItem item) 
            => await _failedItems.InsertOneAsync(item);
        

        public async Task LogAllItems(IEnumerable<FailedItem> items) 
            => await _failedItems.InsertManyAsync(items);

    }
}