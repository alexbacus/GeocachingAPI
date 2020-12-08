using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeocachingAPI.Clients;
using GeocachingAPI.Entities;
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
