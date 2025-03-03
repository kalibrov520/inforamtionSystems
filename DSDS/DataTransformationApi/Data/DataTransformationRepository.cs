﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using DataTransformationApi.Controllers;
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
        
        public async Task PostDataFeedInfoAsync(DataFeedInfo info)
        {
            try
            {
                await _context.DataFeedInfo.AddAsync(info);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //ignored 
            }
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

        //TODO: status! add late and inactive
        public async Task<IList<DataFeedMainInfo>> GetDataFeedsMainInfo(IEnumerable<Guid> deploymentIds)
        {
            var d = _context.DataFeedRunLog.Where(x => deploymentIds.Contains(x.DataFeedId)).ToList();
            //todo add get data feeds by id, not all
            var rows = await (from runLog in _context.DataFeedRunLog
                join fileLog in _context.DataFeedFileLoadingLog 
                    on runLog.FileReadingLogId equals fileLog.FileReadingLogId
                join transformationLog in _context.DataTransformationLog 
                    on fileLog.DataFeedFileLoadingLogId equals transformationLog.DataFeedFileLoadingLogId
                where deploymentIds.Contains(runLog.DataFeedId)
                select new
                {
                    DeploymentId = runLog.DataFeedId,
                    RunId = runLog.RunId,
                    LastRunning = runLog.RunDateTime, 
                    ErrorRecordText = transformationLog.ErrorRecordText,
                    FilePath = fileLog.FilePath
                }).ToListAsync();

            var res = _context.DataFeedRunLog.FirstOrDefault(r => r.RunDateTime == _context.DataFeedRunLog.Max(x => x.RunDateTime));

            var totalRows = res == null ? 0 : _context.DataFeedFileLoadingLog
                                .FirstOrDefault(x => x.FileReadingLogId == res.FileReadingLogId)?.TotalRows;

            var dataFeedList = rows.GroupBy(k => k.DeploymentId, v => v, (k, v) =>
            {
                return new DataFeedMainInfo
                {
                    DeploymentId = k,
                    DataFeedRuns = v.GroupBy(runKey => new {runKey.RunId, runKey.LastRunning}, runValue => runValue,
                        (runKey, runValue) =>
                        {
                            return new DataFeedRunSummaryInfo
                            {
                                RunId = runKey.RunId,
                                RunDate = runKey.LastRunning,
                                Errors = runValue.GroupBy(fileKey => fileKey.FilePath,
                                        fileValue => fileValue.ErrorRecordText)
                                    .ToDictionary(g => g.Key, g => g.ToList())
                            };
                        }).ToList()
                };
            }).ToList();

            foreach (var dataFeed in dataFeedList)
            {
                var lastRun = dataFeed.DataFeedRuns.OrderBy(x => x.RunDate).LastOrDefault();
                if (lastRun != null)
                {
                    dataFeed.FailedRows = lastRun.Errors.Values.Sum(list => list.Count);
                    dataFeed.SuccessRows = (totalRows == null) ? 0 : (int) totalRows - dataFeed.FailedRows;

                    if (lastRun.Errors.Keys.Count == 0)
                    {
                        dataFeed.Status = "late";
                    }
                    else if (dataFeed.FailedRows > 0)
                    {
                        dataFeed.Status = "failed";
                    }
                    else
                    {
                        dataFeed.Status = "success";
                    }
                    dataFeed.LastRunning = lastRun.RunDate;
                }
            }

            return dataFeedList;
        }

        //TODO: rework!
        public async Task<IEnumerable<DataFeedDetailsToReturn>> GetDataFeedDetails(string deploymentId)
        {
            try
            {
                var info = _context.Set<DataFeedDetails>()
                    .FromSql("GetDataFeedDetails @deploymentId = {0}", deploymentId);

                var result = info.Select(r => new DataFeedDetailsToReturn()
                {
                    RunId = r.RunId,
                    Date = r.Date,
                    FailedRows = (r.Files == 0) ? r.FailedRows - 1 : r.FailedRows,
                    Status = StatusChecker(r.Files, r.FailedRows),
                }).ToList();

                foreach (var dataFeed in result)
                {
                    dataFeed.SuccessRows = TotalRowsByRunId(dataFeed.RunId) - dataFeed.FailedRows;
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<string> GetFailedRowsByRunId(string runId)
        {
            var fileReadingLogId = _context.DataFeedRunLog.FirstOrDefault(x => x.RunId.ToString().Equals(runId))?.FileReadingLogId;
            var dataFeedFileLoadingLogId = _context.DataFeedFileLoadingLog
                .FirstOrDefault(x => x.FileReadingLogId.Equals(fileReadingLogId))?.DataFeedFileLoadingLogId;
            var failedRows =
                _context.DataTransformationLog.Where(x => x.DataFeedFileLoadingLogId.Equals(dataFeedFileLoadingLogId)).Select(x => x.ErrorRecordText).ToList();

            return failedRows;
        }

        private int TotalRowsByRunId(Guid runId)
        {
            var filedReadingLogId = _context.DataFeedRunLog.FirstOrDefault(x => x.RunId.Equals(runId))?.FileReadingLogId;

            var totalRows = _context.DataFeedFileLoadingLog.FirstOrDefault(x => x.FileReadingLogId.Equals(filedReadingLogId))?.TotalRows;

            return totalRows ?? 0;
        }

        private string StatusChecker(int filesNumber, int failedRows)
        {
            if (filesNumber == 0)
            {
                return "late";
            }
            else if (failedRows > 0)
            {
                return "failed";
            }
            else
            {
                return "success";
            }
        }

        public async Task LogFileTotalRows(FileTransformationLogRecord logItem)
        {
            try
            {
                var readingFileLogId = await (from fileLog in _context.DataFeedFileLoadingLog
                    join runLog in _context.DataFeedRunLog on fileLog.FileReadingLogId equals runLog.FileReadingLogId
                    where fileLog.FilePath == logItem.FilePath
                    select fileLog.DataFeedFileLoadingLogId).FirstOrDefaultAsync();

                var entity = _context.DataFeedFileLoadingLog.AsNoTracking()
                    .FirstOrDefault(x => x.DataFeedFileLoadingLogId.Equals(readingFileLogId));

                if (entity != null)
                {
                    entity.TotalRows = logItem == null ? 0 : logItem.TotalRows;
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetDataFeedFails(string runId)
        {
            var filedReadingLogId = _context.DataFeedRunLog.FirstOrDefault(x => x.RunId.ToString().Equals(runId))?.FileReadingLogId;

            var dataFeedFileLoadingLogId = _context.DataFeedFileLoadingLog.FirstOrDefault(x => x.FileReadingLogId.Equals(filedReadingLogId))?.DataFeedFileLoadingLogId;

            var result = await _context.DataTransformationLog
                .Where(x => x.DataFeedFileLoadingLogId.Equals(dataFeedFileLoadingLogId)).Select(x => x.ErrorRecordText)
                .ToListAsync();

            return result;
        }
    }
}