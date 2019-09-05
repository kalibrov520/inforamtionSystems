using Microsoft.AspNetCore.Mvc;

namespace LookupApi.Controllers
{
    [Route("api/[controller]")]
    public class PingController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            return "[LookupApi] pong...";
        }

    }
}