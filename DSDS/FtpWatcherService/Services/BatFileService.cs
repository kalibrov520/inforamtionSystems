using System.Threading.Tasks;
using FtpWatcherService.Models;
using MongoDB.Driver;

namespace FtpWatcherService.Services
{
    public class BatFileService
    {
        private readonly IMongoCollection<BatFile> _files;

        public BatFileService(IPoCDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _files = database.GetCollection<BatFile>(settings.PoCCollectionName);
        }

        public Task<IAsyncCursor<BatFile>> GetFileByName(string fileName)
            => _files.FindAsync<BatFile>(file => file.FileName.Equals(fileName));
    }
}