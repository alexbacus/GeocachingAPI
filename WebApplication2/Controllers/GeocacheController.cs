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
        private GeocacheClient client;
        public GeocacheController(GeocachingContext context)
        {
            client = new GeocacheClient(context);
        }

        [HttpGet]
        public List<GeocacheEntity> Get([FromQuery] GeocacheQuery query)
        {
            return client.Get(query);
        }

        [HttpGet("{id}")]
        public GeocacheEntity Get(uint id)
        {
            return client.Get(id);
        }

        [HttpPost]
        public GeocacheEntity Post([FromBody] GeocacheRequest req)
        {
            return client.Save(req);
        }

        [HttpPut("{id}")]
        public GeocacheEntity Put(uint id, GeocacheRequest req)
        {
            return client.Save(id, req);
        }
    }
}
