using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransformationApi.DataModels
{
    public class DataFeedFileLoadingLog
    {
        [Key]
        public Guid DataFeedFileLoadingLogId { get; set; }

        public Guid FileReadingLogId { get; set; }

        public int? TotalRows { get; set; }

        public string FilePath { get; set; }
    }
}