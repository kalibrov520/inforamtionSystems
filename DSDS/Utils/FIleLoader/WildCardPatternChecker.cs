using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using FileLoader.FileSystem;

namespace FileLoader
{
    public class WildCardPatternChecker
    {
        private readonly List<Regex> RegexList = new List<Regex>();

        public WildCardPatternChecker(IEnumerable<string> patterns)
        {
            foreach (var pattern in patterns)
            {
                var regex = new Regex(Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", "."));
                RegexList.Add(regex);
            }
        }

        public bool IsMatch(string path)
        {
            return RegexList.Any(r => r.IsMatch(path));
        }

        public IEnumerable<IFileSystemItem> CheckMatches(IEnumerable<IFileSystemItem> items)
        {
            var result = new List<IFileSystemItem>();
            foreach (var item in items)
            {
                if (IsMatch(item.FullPath))
                {
                    result.Add(item);
                }

                if (item is Directory directory)
                {
                    result.AddRange(CheckMatches(directory.Items));
                }
            }

            return result;
        }
    }
}