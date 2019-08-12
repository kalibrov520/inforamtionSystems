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
                "*.txt",
                "*.csv",
                "*.tsv",
                "*.xls"
            };

            var filesOnFtp = new List<IFileSystemItem>();

            using (var textReader = new StreamReader("FilesList.txt"))
            {
                while (!textReader.EndOfStream)
                {
                    var lineProperties = textReader.ReadLine()?.Split(' ');
                    filesOnFtp.Add(new FileLoader.FileSystem.File
                    {
                        Name = lineProperties[0],
                        FullPath = lineProperties[1],
                        LastModified = Convert.ToDateTime(lineProperties[2] + " " + lineProperties[3] + lineProperties[4])
                    });
                }
            }

            timer.Elapsed += (source, e) =>
            {
                var newFiles = fileLoader.GetFilesWithFileExtensionPattern(patternsForExtension).ToList();

                if (!filesOnFtp.SequenceEqual(newFiles))
                {
                    using (var textWriter = new StreamWriter("FilesList.txt"))
                    {
                        foreach (var item in newFiles)
                        {
                            var lineToWrite = item.Name + " " + item.FullPath + " " + item.LastModified.ToString("g");
                            textWriter.WriteLineAsync(lineToWrite);
                        }
                    }
                    _updateStatus = true;
                }
            };

            timer.Enabled = true;

            while (!_updateStatus)
            {

            }
        }
    }
}