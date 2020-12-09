﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeocachingAPI.Entities
{
    public partial class GeocachingContext
    {
        public DbSet<GeocacheEntity> Geocaches { get; set; }

    }

    [Table("geocaches")]
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
