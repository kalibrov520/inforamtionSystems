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
        public async Task<IEnumerable<DataFeedMainInfo>> GetDataFeedsInfoAsync()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var response = client.DownloadString("http://localhost:8080/engine-rest/process-definition");

                    // TODO convert deploymetnId to GUID

                    var deployment = JArray.Parse(response).Select(x => new
                    {
                        DataFeedId = ((string) x.SelectToken("deploymentId")).Split(":").Last(),
                        DataFeedName = (string) x.SelectToken("name")
                    }).ToDictionary(x => x.DataFeedId, y => y.DataFeedName);

                   

                    var dataFeedList = await _repo.GetDataFeedsMainInfo(deployment.Keys);

                    foreach (var info in dataFeedList)
                    {
                        if (deployment.ContainsKey(info.DeploymentId.ToString()))
                        {
                            info.DataFeed = deployment[info.DeploymentId.ToString()];
                        }
                    }

                    return dataFeedList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<DataFeedDetailsToReturn>> GetDataFeedDetailsInfo(string id)
        {
            try
            {
                return await _repo.GetDataFeedDetails(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}