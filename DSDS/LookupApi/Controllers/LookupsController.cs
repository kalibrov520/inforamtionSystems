using System;
using System.Threading.Tasks;
using LookupApi.Data;
using LookupApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LookupApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILookupRepository _repo;
        private readonly ILogger<LookupsController> _logger;

        public LookupsController(DataContext context, ILookupRepository repo, ILogger<LookupsController> logger)
        {
            _context = context;
            _repo = repo;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        public async Task Post(TalendDocument value)
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
    }
}
