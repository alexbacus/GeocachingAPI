using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeocachingAPI.Entities
{
    public partial class GeocachingContext
    {
        public DbSet<ItemEntity> Items { get; set; }
    }

    [Table("items")]
    public class ItemEntity
    {
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; }

        public uint? CacheId { get; set; }

        [Required]
        public DateTime ActiveStartDate { get; set; }

        [Required]
        public DateTime ActiveEndDate { get; set; }
    }
}
