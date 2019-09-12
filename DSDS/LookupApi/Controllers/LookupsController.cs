using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LookupApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;

namespace LookupApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupRepository _repo;
        private readonly ILogger<LookupsController> _logger;

        public LookupsController(ILookupRepository repo, ILogger<LookupsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        public async Task Post(IEnumerable<TalendResponseObject> value)
        {
            try
            {
                await _repo.PostItems(value);

                _logger.LogInformation("Provided Json was parsed and added to the database {json}", value);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        
        [HttpGet("transferplanlist")]
        public async Task<List<TransferAgentPlan>> GetTransferAgentPlanList()
        {
            try
            {
                return await _repo.GetTransferAgentPlanList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
