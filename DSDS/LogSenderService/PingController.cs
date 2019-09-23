using Microsoft.AspNetCore.Mvc;

namespace LogSenderService
{
    [Route("api/[controller]")]
    public class PingController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            return "[NotificationService] pong...";
        }

    }
}