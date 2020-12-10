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
        [HttpGet]
        public List<ItemEntity> Get([FromQuery] ItemQuery query)
        {
            return ItemClient.Get(query);
        }

        [HttpGet("{id}")]
        public ItemEntity Get(uint id)
        {
            return ItemClient.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] ItemRequest req)
        {
            ItemClient.Save(req);
        }

        // Instead of the typical Save(uint id, [type] columnName) PATCH method, this uses .NET Core JsonPatch
        // This allows for a more dynamic PATCH method that can update any column, instead of just a single specified column
        // It also decreases the load in comparison to a PUT method that requires the entire entity (See GeocacheController PUT)
        [HttpPatch("{id}")]
        public void Save(uint id, [FromBody] JsonPatchDocument<ItemEntity> patchDoc)
        {
            ItemClient.Save(id, patchDoc);
        }
    }
}
