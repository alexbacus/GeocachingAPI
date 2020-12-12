using Microsoft.AspNetCore.Mvc;
using GeocachingAPI.Clients;
using GeocachingAPI.Models;
using GeocachingAPI.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;

namespace GeocachingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ItemClient client;
        public ItemController(GeocachingContext context)
        {
            client = new ItemClient(context);
        }

        [HttpGet]
        public List<ItemEntity> Get([FromQuery] ItemQuery query)
        {
            return client.Get(query);
        }

        [HttpGet("{id}")]
        public ItemEntity Get(uint id)
        {
            return client.Get(id);
        }

        [HttpPost]
        public ItemEntity Post([FromBody] ItemRequest req)
        {
            return client.Save(req);
        }

        // Instead of the typical Save(uint id, [type] columnName) PATCH method, this uses .NET Core JsonPatch
        // This allows for a more dynamic PATCH method that can update any column, instead of just a single specified column
        // It also decreases the load in comparison to a PUT method that requires the entire entity (See GeocacheController PUT)
        [HttpPatch("{id}")]
        public ItemEntity Patch(uint id, [FromBody] JsonPatchDocument<ItemEntity> patchDoc)
        {
            return client.Save(id, patchDoc);
        }
    }
}
