using System;
using System.Collections.Generic;

namespace Models
{
    public class FileReadingLogRecord
    {
        public Guid DataFeedId { get; set; }

        public Guid RunId { get; set; }

        public List<string> FilePathList { get; set; }

    }
}