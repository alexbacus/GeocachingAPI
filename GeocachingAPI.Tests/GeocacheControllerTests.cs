using GeocachingAPI.Controllers;
using GeocachingAPI.Entities;
using GeocachingAPI.Models;
using GeocachingAPI.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GeocachingAPI
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

        [Fact]
        public void GetGeocacheEntity()
        {
            GeocacheQuery query = new GeocacheQuery
            {
                Name = "Test Geocache 2"
            };
            Assert.True(geocacheController.Get(query).Count > 0);
        }
    }
}
