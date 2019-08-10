using FileChecker.FileSystem;

namespace FileChecker
{
    public interface IFileLoader
    {
        IFileSystemItem GetFiles();
    }
}
