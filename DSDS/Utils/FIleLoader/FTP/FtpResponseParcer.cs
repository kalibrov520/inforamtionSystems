using System.Collections.Generic;
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
            //todo last modified date
            return items[2] == DirString ? CreateDirectory(items) : CreateFile(items);
        }


        private static IFileSystemItem CreateDirectory(List<string> items)
        {
            var directory = new Directory
            {
                Name = items[3],
                Items = new List<IFileSystemItem>()
            };
            return directory;
        }

        private static IFileSystemItem CreateFile(List<string> items)
        {
            var file = new File
            {
                Name = items[3],
                Size = double.Parse(items[2])
            };
            return file;
        }
    }
}