using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeocachingAPI.Entities;
using Microsoft.Extensions.Configuration;

namespace GeocachingAPI.Entities
{
    public partial class GeocachingContext : DbContext
    {
        public GeocachingContext() { }

        public GeocachingContext(DbContextOptions<GeocachingContext> options) : base(options)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when creating models in the DB
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GeocacheEntity>()
                .ToTable("geocache");
            builder.Entity<ItemEntity>()
                .ToTable("item");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
