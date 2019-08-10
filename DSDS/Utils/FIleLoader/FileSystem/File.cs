using System;

namespace FileLoader.FileSystem
{
    public class File : IFileSystemItem
    {
        public int Size { get; set; }

        public string Name { get; set; }

        public DateTime LastModified { get; set; }
    }
}