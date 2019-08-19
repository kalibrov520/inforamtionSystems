using System.Collections.Generic;
using System.Threading.Tasks;
using FileLoader.FileSystem;

namespace FileLoader
{
    public interface IFileWriter
    {
        Task WriteFilesOnFileAsync(IEnumerable<IFileSystemItem> files);
    }
}