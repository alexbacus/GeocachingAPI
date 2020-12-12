using GeocachingAPI.Controllers;
using GeocachingAPI.Entities;
using GeocachingAPI.Models;
using GeocachingAPI.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeocachingAPI.Tests
{
    [Collection("Integration Tests")]
    public class GeocacheControllerTests
    {
        GeocachingContext context;
        GeocacheController geocacheController;

        public GeocacheControllerTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder<GeocachingContext>();
            optionsBuilder.UseInMemoryDatabase("TestDB");
            optionsBuilder.UseInternalServiceProvider(serviceProvider);
            context = new GeocachingContext(optionsBuilder.Options);

            geocacheController = new GeocacheController(context);
            SeedData.PopulateTestData(context);
        }

        // Verify successful GET Of all Geocaches in the Geocache table
        [Fact]
        public void GetAllGeocaches()
        {
            GeocacheQuery query = new GeocacheQuery();
            Assert.True(geocacheController.Get(query).Count > 0);
        }

        // Verify successful GET (by name) from Geocache table
        [Fact]
        public void GetGeocacheEntityByName()
        {
            GeocacheQuery query = new GeocacheQuery
            {
                Name = "Test Geocache 2"
            };
            Assert.True(geocacheController.Get(query).Count > 0);
        }

        // Verify successful POST to Geocache table
        [Fact]
        public void AddGeocacheEntity()
        {
            GeocacheRequest request = new GeocacheRequest
            {
                Name = "New Geocache Test",
                Latitude = 0,
                Longitude = 0
            };

            var geocache = geocacheController.Post(request);
            Assert.True(geocache.Id > 0);
        }

        // Verify successful PUT to Geocache table
        [Fact]
        public void UpdateGeocacheEntity()
        {
            GeocacheRequest request = new GeocacheRequest
            {
                Name = "Update Test",
                Latitude = 0,
                Longitude = 0
            };

            var geocache = geocacheController.Put(1, request);
            Assert.True(geocache.Name == "Update Test");
        }
    }
}
