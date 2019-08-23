using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileLoader.FileSystem;

namespace FileLoader.File
{
    public class FileWriter : IFileWriter
    {
        public async Task WriteFilesOnFileAsync(IEnumerable<IFileSystemItem> files)
        {
            using (var textWriter = new StreamWriter(".\\FilesList.txt"))
            {
                foreach (var item in files)
                {
                    var lineToWrite = item.Name + " " + item.FullPath + " " + item.LastModified.ToString("O");

                    await textWriter.WriteLineAsync(lineToWrite);
                }
            }
        }
    }
}