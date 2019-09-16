using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransformationApi.DataModels
{
    public class DataFeedRunLog
    {
        [Key]
        public Guid FileReadingLogId { get; set; }

        public Guid DataFeedId { get; set; }

        public Guid RunId { get; set; }

        public DateTime RunDateTime { get; set; }
    }
}