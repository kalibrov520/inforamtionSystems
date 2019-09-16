using System;

namespace Models
{
    public class DataFeedMainInfo
    {
        public string DeploymentId { get; set; }
        public string DataFeed { get; set; }
        public DateTime LastRunning { get; set; }
        public string Status { get; set; }
        public int SuccessRows { get; set; }
        public int FailedRows { get; set; }
    }
}