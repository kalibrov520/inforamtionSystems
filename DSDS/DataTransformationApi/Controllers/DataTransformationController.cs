using System;
using System.Threading.Tasks;
using DataTransformationApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

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
        public async Task<IActionResult> GetDataFeedsInfoAsync()
        {
            throw new NotImplementedException();
        }
    }
}