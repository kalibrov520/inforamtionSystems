﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FtpWatcherService
{
    [Route("api/[controller]")]
    public class PingController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Ping()
        {
            return "[Ftp Watcher] Pong...";
        }

    }
}
