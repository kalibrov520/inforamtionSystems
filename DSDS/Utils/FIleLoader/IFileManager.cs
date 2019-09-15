using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader
{
    public interface IFileManager
    {
        string GetPathForFile(string filename, Guid dataFeedId, Guid runID);

        Task<string> SaveFileAsync(Guid dataFeedId, Guid runID, string fileName, byte[] data);

        Task<byte[]> GetFileAsync(string fullFileName);

    }
}
