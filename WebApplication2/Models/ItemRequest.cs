using GeocachingAPI.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeocachingAPI.Models
{
    public class ItemRequest
    {
        [Required]
        public string Name { get; set; }

        public uint? CacheId { get; set; }

        [Required]
        public DateTime ActiveStartDate { get; set; }

        [Required]
        public DateTime ActiveEndDate { get; set; }
    }
}
