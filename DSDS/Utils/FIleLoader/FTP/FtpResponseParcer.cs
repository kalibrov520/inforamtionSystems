using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using FileLoader.FileSystem;

namespace FileLoader.FTP
{
    public class FtpResponseParcer
    {
        private const char ParserDelimeter = ' ';
        private const string DirString = "<DIR>";


        public static IFileSystemItem ParseLine(string responseLine)
        {
            if (string.IsNullOrEmpty(responseLine))
                return null;

            var items = responseLine.Split(ParserDelimeter).Where(x => !string.IsNullOrEmpty(x)).ToList();

            if (items.Count < 4)
                return null;

            //var result = items[2] == DirString ? CreateDirectory(items) : CreateFile(items);
            //var dateString = $"{items[0]} {items[1]}";
            var result = items[0].StartsWith("d") ? CreateDirectory(items) : CreateFile(items);
            var dateString = $"{DateTime.Now.Year} {items[5]} {items[6]} {items[7]}";
            var lastModifiedDateTime = DateTime.ParseExact(dateString, "yyyy MMM dd HH:mm", CultureInfo.InvariantCulture);
            result.LastModified = lastModifiedDateTime;
            return result;
        }


        private static IFileSystemItem CreateDirectory(IList<string> items)
        {
            var directory = new Directory
            {
                Name = items[8],
                Items = new List<IFileSystemItem>()
            };
            return directory;
        }

        private static IFileSystemItem CreateFile(IList<string> items)
        {
            var file = new FileSystem.File
            {
                Name = items[8],
                Size = double.Parse(items[4])
            };
            return file;
        }
    }
}