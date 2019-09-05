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
        private readonly ILogger<LookupsController> _logger;

        public LookupsController(DataContext context, ILogger<LookupsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            try
            {
                var talendDocument = JsonConvert.DeserializeObject<TalendDocument>(value);

                await _context.Items.AddRangeAsync(talendDocument.SuccessfulItems);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Provided Json was parsed and added to the database {json}", value);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}
