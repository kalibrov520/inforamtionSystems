using System;
using System.Collections.Generic;

namespace FileChecker.FileSystem
{
    public class Directory : IFileSystemItem
    {
        public string FullPath { get; set; }

        public IEnumerable<IFileSystemItem> Items { get; set; }

        public string Name { get; set; }

        public DateTime LastModified { get; set; }
    }
}