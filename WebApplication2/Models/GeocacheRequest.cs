using System.ComponentModel.DataAnnotations;

namespace GeocachingAPI.Models
{
    public class GeocacheRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public float Latitude { get; set; }
    }
}
