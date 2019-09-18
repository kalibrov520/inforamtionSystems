using System;
using System.Collections.Generic;

namespace Models
{
    public class FileTransformationLogRecord
    {
        public Guid DataFeedId { get; set; }

        public Guid RunId { get; set; }

        public string FilePath { get; set; }

        public int? TotalRows { get; set; }

        public List<string> InvalidRows { get; set; }
    }
}