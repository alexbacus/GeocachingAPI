//using GeocachingAPI.Entities;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Threading.Tasks;
//using Xunit;

//namespace GeocachingTestProject
//{
//    [Collection("Integration Tests")]
//    public class GeocacheControllerTests
//    {
//        private readonly WebApplicationFactory<Startup> _factory;

//        public GeocacheControllerTests(WebApplicationFactory<Startup> factory)
//        {
//            _factory = factory;

//            var dbContext = _factory.Services.GetRequiredService<GeocachingContext>();


//            var geocache = new GeocacheEntity
//            {
//                Name = "Test Geocache 1",
//                Latitude = 0,
//                Longitude = 0
//            };
//            dbContext.Geocache.Add(geocache);
//            var startDate = DateTime.Today;
//            var endDate = DateTime.Today.AddDays(2);
//            var item = new ItemEntity
//            {
//                Name = "Test Item 1",
//                CacheId = null,
//                ActiveStartDate = startDate,
//                ActiveEndDate = endDate
//            };
//            dbContext.Item.Add(item);
//            dbContext.SaveChanges();
//        }

//        [Fact]
//        public async Task GetGeocacheEntity()
//        {
//            // Arrange
//            var client = _factory.CreateClient();

//            // Act
//            var response = await client.GetAsync("/Geocache?Name=Test%20Geocache%201");

//            //Assert
//            response.EnsureSuccessStatusCode();
//            Assert.NotNull(response.Content);
//        }
//    }
//}
