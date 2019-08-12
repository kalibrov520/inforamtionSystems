using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FileLoader.FileSystem;
using FileLoader.FTP;
using File = System.IO.File;

namespace FileCheckerTestConsoleApp
{
    public class FromFileDemo
    {
        public static void Run()
        {
            var _updateStatus = false;

            var timer = new System.Timers.Timer(10000); // every 10 seconds

            var fileLoader = new FtpFileLoader("ftp://spb-mdspoc01.internal.corp", "ftpUser", "password123");

            var patternsForExtension = new List<string>
            {
                "*.txt"
            };

            var filesOnFtp = fileLoader.GetFilesWithFileExtensionPattern(patternsForExtension);

            timer.Elapsed += (source, e) =>
            {
                var newFiles = fileLoader.GetFilesWithFileExtensionPattern(patternsForExtension).ToList();

                var check = filesOnFtp.SequenceEqual(newFiles);

                if (check)
                {
                    using (var textWriter = new StreamWriter("FilesList.txt"))
                    {
                        foreach (var item in newFiles)
                        {
                            var lineToWrite = item.Name + " " + item.FullPath + " " + item.LastModified.ToString("g");
                            textWriter.WriteLineAsync(lineToWrite);
                        }
                    }
                    using (var textReader = new StreamReader("FilesList.txt"))
                    {
                        var files = new List<IFileSystemItem>();
                        while (!textReader.EndOfStream)
                        {
                            var lineProperties = textReader.ReadLine()?.Split(' ');
                            files.Add(new FileLoader.FileSystem.File
                            {
                                Name = lineProperties[0],
                                FullPath = lineProperties[1],
                                LastModified = Convert.ToDateTime(lineProperties[2] + " " + lineProperties[3] + lineProperties[4])
                            });
                        }
                    }
                    _updateStatus = true;
                }
            };

            var firstDictionary = filesOnFtp.ToDictionary(entry => entry.Name, entry => entry.LastModified);

            timer.Enabled = true;

            while (!_updateStatus)
            {

            }

            timer.Enabled = false;
        }
    }
}