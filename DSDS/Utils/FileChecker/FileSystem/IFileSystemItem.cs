using System;

namespace FileChecker.FileSystem
{
    public interface IFileSystemItem
    {
        string Name { get; set; }

        DateTime LastModified { get; set; }
    }
}