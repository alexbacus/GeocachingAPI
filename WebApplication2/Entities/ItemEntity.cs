using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeocachingAPI.Entities
{
    public partial class GeocachingContext
    {
        public DbSet<ItemEntity> Item { get; set; }
    }

    [Table("item")]
    public class ItemEntity
    {
        public uint Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public uint? CacheId { get; set; } = null;

        [Required]
        public DateTime ActiveStartDate { get; set; }

        [Required]
        public DateTime ActiveEndDate { get; set; }
    }
}
