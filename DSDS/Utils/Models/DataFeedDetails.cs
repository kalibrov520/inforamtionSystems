using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class DataFeedDetails
    {
        [Key]
        public Guid RunId { get; set; }
        public DateTime Date { get; set; }
        public int FailedRows { get; set; }
        public int Files { get; set; }
    }

    public class DataFeedDetailsToReturn
    {
        public Guid RunId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public int FailedRows { get; set; }
        public int SuccessRows { get; set; }
    }
}