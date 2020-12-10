using GeocachingAPI.Entities;
using GeocachingAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeocachingAPI.Clients
{
    public class ItemClient
    {
        // Return a single Geocache based on specified unique identifier
        public static ItemEntity Get(uint id)
        {
            using (var db = new GeocachingContext())
            {
                var result = db.Item.FirstOrDefault(x => x.Id == id);

                if (result == null)
                {
                    // throw error message if a Geocache was not found with specified ID
                    throw new ArgumentException("Item with id: " + id + " was not found.");
                }

                return result;
            }
        }

        // Create and add a new Item to the DB
        // CacheId can be left null
        public static ItemEntity Save(ItemRequest request)
        {
            using (var db = new GeocachingContext())
            {
                // If the request contains a Geocache ID, verify that it does not already contain 3 active Items
                // If the Geocache contains 3 active items, throw an Exception and do not save the new Item to the DB
                if (request.CacheId.HasValue)
                {
                    if (CheckItemsInCache((uint)request.CacheId) == 3)
                    {
                        throw new Exception("The specified Geocache already contains 3 active items.");
                    }
                }
                var item = new ItemEntity
                {
                    Name = request.Name,
                    CacheId = request.CacheId,
                    ActiveStartDate = request.ActiveStartDate,
                    ActiveEndDate = request.ActiveEndDate
                };

                db.Item.Add(item);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.InnerException.Message);
                }
                db.SaveChanges();
                return item;
            }
        }

        // Get all items in the DB
        // If a GeocacheId is specified in the query, returns only active items that belong to that geocache
        public static List<ItemEntity> Get(ItemQuery query)
        {
            var now = DateTime.UtcNow;

            using (var db = new GeocachingContext())
            {
                var result = db.Item.AsNoTracking();


                if (query.GeocacheId.HasValue)
                {
                    /* This query filters inactive items out by default when a cache ID is specified
                        This can be modified in the future as needed to return inactive/active/all items
                        This can be done by adding a "status" query parameter and checking against it, and then filtering by cache ID as needed */
                    result = result.Where(x => x.CacheId == query.GeocacheId &&
                                x.ActiveStartDate < now && x.ActiveEndDate > now);
                }

                return result.ToList();
            }
        }

        // Update a column of a single Item entity
        // The patchDoc parameter will look like the following:
            /* [ { "op": "replace", "path": "/cacheId", "value": null } ]
             * op: the operation to be performed
             * path: the column name that is to be changed
             * value: the new value for the column
             * this example would set a item entity's cache ID to null
            */
        // https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-3.1#json-patch
        public static ItemEntity Save(uint id, [FromBody] JsonPatchDocument<ItemEntity> patchDoc)
        {
            var now = DateTime.UtcNow;

            using (var db = new GeocachingContext())
            {
                var entity = db.Item.First(x => x.Id == id);

                // get old values before updating to check for inactive Items
                var oldEntity = db.Item.AsNoTracking().First(x => x.Id == id);
                var isActive = (oldEntity.ActiveStartDate < now && oldEntity.ActiveEndDate > now);

                if (entity == null)
                {
                    throw new ArgumentException("Item with id: " + id + " was not found.");
                }

                patchDoc.ApplyTo(entity);

                if (entity.CacheId.HasValue)
                {
                    // only active items can be moved
                    if (oldEntity.CacheId != entity.CacheId && !isActive)
                    {
                        throw new Exception("Only active items can be moved.");
                    }
                    // active items can only be moved to Geocaches with less than 3 existing active items
                    if (CheckItemsInCache((uint)entity.CacheId) == 3)
                    {
                        throw new Exception("The specified Geocache already contains 3 active items.");
                    }
                }

                db.SaveChanges();
                return entity;
            }
        }

        // Update the Geocache an Item belongs to (or set to null to remove the Item from Geocache)
        // If Geocache ID is not null, verify that the Geocache does not already have 3 associated Items
        public static ItemEntity Save(uint itemId, uint? geocacheId)
        {
            using (var db = new GeocachingContext())
            {
                var item = db.Item.First(x => x.Id == itemId);

                if (geocacheId.HasValue)
                {
                    if (CheckItemsInCache((uint)geocacheId) < 3)
                    {
                        item.CacheId = geocacheId;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The specified Geocache already contains 3 active items.");
                    }
                }
                else
                {
                    item.CacheId = null;
                }

                return item;
            }
        }

        // This method returns the count of active Items currently assigned to a Geocache
        public static int CheckItemsInCache(uint geocacheId)
        {
            var query = new ItemQuery
            {
                GeocacheId = geocacheId
            };

            return Get(query).Count;
        }
    }
}
