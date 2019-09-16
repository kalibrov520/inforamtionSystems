using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTransformationApi.Data;
using DataTransformationApi.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public IEnumerable<DataFeedMainInfo> GetDataFeedsInfoAsync()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var deployment = new Dictionary<string, string>();
                    var response = client.DownloadString("http://localhost:8080/engine-rest/process-definition");

                    foreach (var processDefinition in JArray.Parse(response))
                    {
                        deployment.Add((string) processDefinition.SelectToken("deploymentId"),
                            (string) processDefinition.SelectToken("name"));
                    }

                    //var fullInfo = await _repo.GetDataFeedsMainInfo(deployment.Keys);

                    var result = @"[
                            {
                                ""deploymentId"": ""7CBD1348-D875-11E9-BD45-0242AC110002"",
                                ""status"": ""failed"",
                                ""dataFeed"": ""State Street Data Feed"",
                                ""lastRunning"": ""02/09/2019"",
                                ""successRows"": 100,
                                ""failedRows"": 2 
                            },
                            {
                                ""deploymentId"": ""7CBD1348-D875-11E9-BD45-0242AC110003"",
                                ""status"": ""failed"",
                                ""dataFeed"": ""Open Finance Data Feed"",
                                ""lastRunning"": ""01/08/2019"",
                                ""successRows"": 75,
                                ""failedRows"": 15 
                            },
                            {
                                ""deploymentId"": ""7CBD1348-D875-11E9-BD45-0242AC110004"",
                                ""status"": ""success"",
                                ""dataFeed"": ""Capital Street Data Feed"",
                                ""lastRunning"": ""03/09/2019"",
                                ""successRows"": 234,
                                ""failedRows"": 0 
                            }
                        ]";

                    return JsonConvert.DeserializeObject<List<DataFeedMainInfo>>(result);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}