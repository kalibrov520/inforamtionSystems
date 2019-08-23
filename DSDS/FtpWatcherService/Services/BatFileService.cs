using System.Collections.Generic;
using System.Linq;
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

        public BatFile GetFileByName(string fileName)
            => _files.FindAsync<BatFile>(file => file.FileName == fileName).Result.Current.FirstOrDefault();

        public IAsyncEnumerable<BatFile> GetBatFilesAsync()
            => _files.FindAsync(file => true).Result.Current.ToAsyncEnumerable();

        public List<BatFile> GetBatFiles() 
            => _files.Find(file => true).ToList();
    }
}