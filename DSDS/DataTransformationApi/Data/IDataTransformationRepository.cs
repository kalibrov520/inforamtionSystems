using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransformationApi.Controllers;
using DataTransformationApi.DataModels;
using Models;

namespace DataTransformationApi.Data
{
    public interface IDataTransformationRepository
    {
        Task PostDataFeedInfoAsync(DataFeedInfo info);
        
        Task LogFileReading(FileReadingLogRecord logItem);

        Task LogDataTransformation(FileTransformationLogRecord logItem);

        Task<IList<DataFeedMainInfo>> GetDataFeedsMainInfo(
            IEnumerable<Guid> deploymentIds);

        Task<IEnumerable<DataFeedDetailsToReturn>> GetDataFeedDetails(string deploymentId);

        IEnumerable<string> GetFailedRowsByRunId(string runId);

        Task LogFileTotalRows(FileTransformationLogRecord logItem);
    }
}