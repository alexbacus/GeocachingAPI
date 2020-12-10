using Microsoft.AspNetCore.Mvc;
using GeocachingAPI.Clients;
using GeocachingAPI.Models;
using GeocachingAPI.Entities;
using System.Collections.Generic;

namespace GeocachingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocacheController : ControllerBase
    {
        [HttpGet]
        public List<GeocacheEntity> Get([FromQuery] GeocacheQuery query)
        {
            return GeocacheClient.Get(query);
        }

        [HttpGet("{id}")]
        public GeocacheEntity Get(uint id)
        {
            return GeocacheClient.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] GeocacheRequest req)
        {
            GeocacheClient.Save(req);
        }
    }
}
