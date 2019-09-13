using DataTransformationApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    }
}