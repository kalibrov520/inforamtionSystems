using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class DataFeedInfo
    {
        [Key]
        public int Id { get; set; }

        public string Status { get; set; }

        public string DataFeed { get; set; }

        public DateTime LastRunning { get; set; }

        public int SuccessRows { get; set; }

        public int FailedRows { get; set; }
    }
}