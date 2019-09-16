using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransformationApi.DataModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataTransformationApi.Data
{
    public class DataTransformationRepository : IDataTransformationRepository
    {
        private readonly DataContext _context;

        public DataTransformationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task LogFileReading(FileReadingLogRecord logItem)
        {
            var runLogItem = new DataFeedRunLog
            {
                FileReadingLogId = Guid.NewGuid(),
                DataFeedId = logItem.DataFeedId,
                RunId = logItem.RunId,
                RunDateTime = DateTime.Now
            };
            await _context.DataFeedRunLog.AddAsync(runLogItem);
            

            var fileLogItems = logItem.FilePathList.Select(file => new DataFeedFileLoadingLog
            {
                DataFeedFileLoadingLogId = Guid.NewGuid(),
                FileReadingLogId = runLogItem.FileReadingLogId,
                FilePath = file
            }).ToList();

            await _context.DataFeedFileLoadingLog.AddRangeAsync(fileLogItems);
            await _context.SaveChangesAsync();
        }

        public async Task LogDataTransformation(FileTransformationLogRecord logItem)
        {
            var readingFileLogId = await (from fileLog in _context.DataFeedFileLoadingLog
                join runLog in _context.DataFeedRunLog on fileLog.FileReadingLogId equals runLog.FileReadingLogId
                where fileLog.FilePath == logItem.FilePath
                select fileLog.DataFeedFileLoadingLogId).FirstOrDefaultAsync();

            var items = logItem.InvalidRows.Select(x => new DataTransformationLog
            {
                DataTransformationLogId = Guid.NewGuid(),
                DataFeedFileLoadingLogId = readingFileLogId,
                ErrorRecordText = x
            });

            await _context.DataTransformationLog.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }
    }
}