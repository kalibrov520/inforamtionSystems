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

        //Task<IEnumerable<>> GetDataFeedsMainInfo(IEnumerable<string> deploymentIds);

    }
}