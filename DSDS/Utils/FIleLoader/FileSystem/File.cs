using System;
using System.Numerics;

namespace FileLoader.FileSystem
{
    public class File : IFileSystemItem
    {
        public double Size { get; set; }

        public string FullPath { get; set; }

        public string Name { get; set; }

        public DateTime LastModified { get; set; }
    }
}