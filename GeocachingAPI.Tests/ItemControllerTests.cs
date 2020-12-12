using GeocachingAPI.Controllers;
using GeocachingAPI.Entities;
using GeocachingAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace GeocachingAPI.Tests
{
    [Collection("Integration Tests")]
    public class ItemControllerTests
    {
        GeocachingContext context;
        ItemController itemController;

        public ItemControllerTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder<GeocachingContext>();
            optionsBuilder.UseInMemoryDatabase("TestDB");
            optionsBuilder.UseInternalServiceProvider(serviceProvider);
            context = new GeocachingContext(optionsBuilder.Options);

            itemController = new ItemController(context);
            SeedData.PopulateTestData(context);
        }

        // Verify successful GET of all Items in Item table
        [Fact]
        public void GetAllItems()
        {
            ItemQuery query = new ItemQuery
            {
                
            };
            Assert.True(itemController.Get(query).Count > 0);
        }

        // Verify successful POST to Item table
        [Fact]
        public void AddItemEntity()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(2);

            var request = new ItemRequest
            {
                Name = "Test Item 10",
                CacheId = null,
                ActiveStartDate = startDate,
                ActiveEndDate = endDate
            };

            var item = itemController.Post(request);
            Assert.True(item.Id > 0);
        }

        // Verify successful PATCH to Item table
        [Fact]
        public void UpdateGeocacheEntity()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(2);
            var patch = new JsonPatchDocument<ItemEntity>();
            patch = patch.Replace(i => i.Name, "Test Patch");

            var item = itemController.Patch(1, patch);
            Assert.True(item.Name == "Test Patch");
        }
    }
}
