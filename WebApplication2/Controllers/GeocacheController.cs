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
        public void Post([FromBody] GeocacheRequest req)
        {
            client.Save(req);
        }

        [HttpPut("{id}")]
        public void Save(uint id, GeocacheRequest req)
        {
            client.Save(id, req);
        }
    }
}
