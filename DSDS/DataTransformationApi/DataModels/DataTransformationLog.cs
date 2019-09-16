using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransformationApi.DataModels
{
    public class DataTransformationLog
    {
        [Key]
        public Guid DataTransformationLogId { get; set; }

        public Guid DataFeedFileLoadingLogId { get; set; }

        public string ErrorRecordText { get; set; }
    }
}