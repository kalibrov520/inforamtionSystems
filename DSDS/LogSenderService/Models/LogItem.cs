using System;
using System.Collections.Generic;
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

        public IEnumerable<SuccessfulItem> SuccessfulItems { get; set; }

        public IEnumerable<FailedItem> FailedItems { get; set; }

        public DateTime StartDate { get; set; }
    }

    public class SuccessfulItem
    {

    }

    public class FailedItem
    {

    }
}