using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeocachingAPI.Entities
{
    public partial class GeocachingContext
    {
        public DbSet<GeocacheEntity> Geocaches { get; set; }

    }

    [Table("daily_data")]
    public class GeocacheEntity
    {
        public uint Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public float Latitude { get; set; }
    }
}
