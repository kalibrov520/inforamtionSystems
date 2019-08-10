using System;

namespace FileLoader.FileSystem
{
    public interface IFileSystemItem
    {
        string Name { get; set; }

        DateTime LastModified { get; set; }
    }
}