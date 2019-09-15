using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileLoader.FileSystem;

namespace FileLoader
{
    public class FileSystemFileChecker : IFileChecker
    {
        private IEnumerable<IFileSystemItem> GetFiles()
        {
            var filesOnFtp = new List<IFileSystemItem>();

            if (!System.IO.File.Exists(".\\FilesList.txt"))
            {
                System.IO.File.Create(".\\FilesList.txt");
            }

            using (var textReader = new StreamReader(".\\FilesList.txt"))
            {
                while (!textReader.EndOfStream)
                {
                    var lineProperties = textReader.ReadLine()?.Split(' ');
                    filesOnFtp.Add(new FileSystem.File
                    {
                        Name = lineProperties?[0],
                        FullPath = lineProperties?[1],
                        LastModified = DateTime.Parse(lineProperties?[2])
                        //Convert.ToDateTime(lineProperties?[2])
                    });
                }
            }

            return filesOnFtp;
        }

        public IEnumerable<IFileSystemItem> GetNewFileList(IEnumerable<IFileSystemItem> source)
        {
            var processedFileList = GetFiles();
            return source.Where(x => !processedFileList.Contains(x)).ToList();
        }

        public async Task WriteNewFilesOnFileAsync(IEnumerable<IFileSystemItem> files)
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