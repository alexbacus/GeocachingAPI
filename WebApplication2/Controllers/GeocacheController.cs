﻿using Microsoft.AspNetCore.Mvc;
using GeocachingAPI.Clients;
using GeocachingAPI.Models;

namespace GeocachingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocacheController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] GeocacheRequest req)
        {
            GeocacheClient.Save(req);
        }
    }
}
