using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogService.Models
{
    public class LogItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        public bool IsSucceeded { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string SuccessfulItems { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string FailedItems { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public BsonDateTime StartDate { get; set; }
    }
}