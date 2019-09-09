using System.Collections.Generic;
using System.Threading.Tasks;
using LogService.Models;
using MongoDB.Driver;

namespace LogService.Services
{
    public class LogService
    {
        private readonly IMongoCollection<LogItem> _logItems;

        public LogService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _logItems = database.GetCollection<LogItem>(settings.CollectionName);
        }

        public async Task LogSingleItem(LogItem item)
            => await _logItems.InsertOne(item);


        public async Task LogAllItems(IEnumerable<LogItem> items)
            => await _logItems.InsertManyAsync(items);
    }
}