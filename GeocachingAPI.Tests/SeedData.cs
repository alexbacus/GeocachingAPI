using GeocachingAPI.Entities;
using System;

namespace GeocachingAPI.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(GeocachingContext dbContext)
        {
            // create and add 3 new geocaches
            var geocache = new GeocacheEntity
            {
                Name = "Test Geocache 1",
                Latitude = 0,
                Longitude = 0
            };
            dbContext.Geocache.Add(geocache);
            geocache = new GeocacheEntity
            {
                Name = "Test Geocache 2",
                Latitude = 0,
                Longitude = 0
            };
            dbContext.Geocache.Add(geocache);
            geocache = new GeocacheEntity
            {
                Name = "Test Geocache 3",
                Latitude = 0,
                Longitude = 0
            };
            dbContext.Geocache.Add(geocache);
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(2);

            // create and add 3 items that are not past their active end date (active)
            var item = new ItemEntity
            {
                Name = "Test Item 1",
                CacheId = null,
                ActiveStartDate = startDate,
                ActiveEndDate = endDate
            };
            dbContext.Item.Add(item);

            item = new ItemEntity
            {
                Name = "Test Item 2",
                CacheId = null,
                ActiveStartDate = startDate,
                ActiveEndDate = endDate
            };
            dbContext.Item.Add(item);

            item = new ItemEntity
            {
                Name = "Test Item 3",
                CacheId = null,
                ActiveStartDate = startDate,
                ActiveEndDate = endDate
            };
            dbContext.Item.Add(item);

            // create and add an item that is past its active end date (no longer active)
            endDate = endDate.AddDays(-4);
            item = new ItemEntity
            {
                Name = "Test Item 3",
                CacheId = null,
                ActiveStartDate = startDate,
                ActiveEndDate = endDate
            };
            dbContext.Item.Add(item);
            dbContext.SaveChanges();
        }
    }
}
