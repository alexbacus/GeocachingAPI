using GeocachingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeocachingAPI.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(GeocachingContext dbContext)
        {
            var geocache = new GeocacheEntity
            {
                Name = "Test Geocache 2",
                Latitude = 0,
                Longitude = 0
            };
            dbContext.Geocache.Add(geocache);
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(2);
            var item = new ItemEntity
            {
                Name = "Test Item 2",
                CacheId = null,
                ActiveStartDate = startDate,
                ActiveEndDate = endDate
            };
            dbContext.Item.Add(item);
            dbContext.SaveChanges();
        }
    }
}
