using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileLoader
{
    public  class FileSystemFileManager : IFileManager
    {
        // todo move to config
        private const string FileStorageRootPath = "C:\\dsds_files";

        public string GetPathForFile(string filename, Guid dataFeedId, Guid runID)
        {
            var safeFilePath = Regex.Replace(filename, "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", "_");
           return $"{FileStorageRootPath}\\{dataFeedId.ToString()}\\{runID.ToString()}\\{safeFilePath}";
        }

        public async Task<string> SaveFileAsync(Guid dataFeedId, Guid runID, string fileName, byte[] data)
        {
            var path = GetPathForFile(fileName, dataFeedId, runID);

            try
            {
                var folderName = Path.GetDirectoryName(path);

                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                await System.IO.File.WriteAllBytesAsync(path, data);
                return path;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<byte[]> GetFileAsync(string fullFileName)
        {
            try
            {
                if (!System.IO.File.Exists(fullFileName))
                {
                    throw  new FileNotFoundException();
                }

                return await System.IO.File.ReadAllBytesAsync(fullFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
