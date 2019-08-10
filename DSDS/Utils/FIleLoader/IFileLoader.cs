using FileLoader.FileSystem;

namespace FileLoader
{
    public interface IFileLoader
    {
        IFileSystemItem GetFiles();
    }
}
