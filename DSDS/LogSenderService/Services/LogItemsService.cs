using System.Collections.Generic;
using System.Threading.Tasks;
using LogSenderService.Models;
using MongoDB.Driver;

namespace LogSenderService.Services
{
    public class LogItemsService
    {
        private readonly IMongoCollection<LogItem> _logItems;

        public LogItemsService(IDatabaseSettings settings)
        {
            var client  = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _logItems = database.GetCollection<LogItem>(settings.CollectionName);
        }

        public async Task LogSingleItemAsync(LogItem item) 
            => await _logItems.InsertOneAsync(item);
        

        public async Task LogAllItemsAsync(IEnumerable<LogItem> items) 
            => await _logItems.InsertManyAsync(items);

    }
}