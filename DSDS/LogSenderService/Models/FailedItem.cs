using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogSenderService.Models
{
    public class FailedItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }

        public string Content { get; set; }
    }
}