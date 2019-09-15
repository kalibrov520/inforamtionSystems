using System.Collections.Generic;
using System.Threading.Tasks;
using FileLoader.FileSystem;

namespace FileLoader
{
    public interface IFileChecker
    {
        IEnumerable<IFileSystemItem> GetNewFileList(IEnumerable<IFileSystemItem> source);

        Task WriteNewFilesOnFileAsync(IEnumerable<IFileSystemItem> files);
    }
}
