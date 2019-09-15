using System.Collections.Generic;
using FileLoader.FileSystem;

namespace FileLoader
{
    public interface IFileLoader
    {
        IEnumerable<IFileSystemItem> GetFiles();

        byte[] GetFileContent(string fullPath);

        IEnumerable<IFileSystemItem> GetFilesWithPattern(IEnumerable<string> patterns);

        IEnumerable<IFileSystemItem> GetFilesWithFileExtensionPattern(IEnumerable<string> patterns);
    }
}
