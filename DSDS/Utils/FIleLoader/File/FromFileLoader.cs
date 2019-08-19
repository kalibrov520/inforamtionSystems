using System;
using System.Collections.Generic;
using System.IO;
using FileLoader.FileSystem;

namespace FileLoader.File
{
    public class FromFileLoader : IFileLoader
    {
        public IEnumerable<IFileSystemItem> GetFiles()
        {
            var filesOnFtp = new List<IFileSystemItem>();

            if (!System.IO.File.Exists(".\\FilesList.txt"))
            {
                using (System.IO.File.Create(".\\FilesList.txt"))
                {
                }
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
                        LastModified =
                            Convert.ToDateTime(lineProperties?[2] + " " + lineProperties?[3] + lineProperties?[4])
                    });
                }
            }

            return filesOnFtp;
        }
    }
}