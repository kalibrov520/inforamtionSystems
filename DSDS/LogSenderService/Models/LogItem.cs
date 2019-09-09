using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogSenderService.Models
{
    public class LogItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool IsSucceeded { get; set; }

        public string SuccessfulItems { get; set; }

        public string FailedItems { get; set; }

        public DateTime StartDate { get; set; }
    }
}