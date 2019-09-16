using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DataTransformationApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;

namespace DataTransformationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTransformationController
    {
        private readonly IDataTransformationRepository _repo;
        private readonly ILogger<DataTransformationController> _logger;

        public DataTransformationController(IDataTransformationRepository repo, ILogger<DataTransformationController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost]
        public async Task PostDataFeedInfoAsync(DataFeedInfo info)
        {
            try
            {
                await _repo.PostDataFeedInfoAsync(info);
            }
            catch (Exception e)
            {
                //ignored
            }
        }

        [HttpGet]
        public async Task<IEnumerable<DataFeedInfoToReturn>> GetDataFeedsInfoAsync()
        {
            using (var client = new WebClient())
            {
                var response = client.DownloadString("http://localhost:8080/engine-rest/process-definition");

            }
            var result = @"[
                            {
                                ""status"": ""failed"",
                                ""dataFeed"": ""State Street Data Feed"",
                                ""lastRunning"": ""02/09/2019"",
                                ""successRows"": 100,
                                ""failedRows"": 2 
                            },
                            {
                                ""status"": ""failed"",
                                ""dataFeed"": ""Open Finance Data Feed"",
                                ""lastRunning"": ""01/08/2019"",
                                ""successRows"": 75,
                                ""failedRows"": 15 
                            },
                            {
                                ""status"": ""success"",
                                ""dataFeed"": ""Capital Street Data Feed"",
                                ""lastRunning"": ""03/09/2019"",
                                ""successRows"": 234,
                                ""failedRows"": 0 
                            }
                        ]";

            return JsonConvert.DeserializeObject<IEnumerable<DataFeedInfoToReturn>>(result);
        }
    }
}