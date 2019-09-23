using System;
using System.Collections.Generic;

namespace Models
{
    public class DataFeedMainInfo
    {
        public Guid DeploymentId { get; set; }
        public string ProcessDefinitionId { get; set; }
        public Guid LastRunId { get; set; }
        public string DataFeed { get; set; }
        public DateTime LastRunning { get; set; }
        public string Status { get; set; }
        public int SuccessRows { get; set; }
        public int FailedRows { get; set; }
        public IEnumerable<DataFeedRunSummaryInfo> DataFeedRuns { get; set; }
    }

    public class DataFeedRunSummaryInfo
    {
        public Guid RunId { get; set; }

        public DateTime RunDate { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }
    }

}