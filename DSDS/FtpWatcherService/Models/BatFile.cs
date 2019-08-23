using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FtpWatcherService.Models
{
    public class BatFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FileName { get; set; }

        public string FileContent { get; set; }
    }
}