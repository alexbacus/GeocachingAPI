using System;
using System.ComponentModel.DataAnnotations;

namespace GeocachingAPI.Models
{
    public class ItemRequest
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(Constants.ItemNamePattern, 
            ErrorMessage = "Only letters, numbers, and white spaces are allowed.")]
        public string Name { get; set; }

        public uint? CacheId { get; set; }

        [Required]
        public DateTime ActiveStartDate { get; set; }

        [Required]
        public DateTime ActiveEndDate { get; set; }
    }
}
