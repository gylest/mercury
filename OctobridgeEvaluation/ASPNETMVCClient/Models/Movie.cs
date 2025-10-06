using System.ComponentModel.DataAnnotations;

namespace MVCClient.Models
{
    public class Movie
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Star { get; set; }
        [Required]
        [Range(1950, 2025)]
        public int YearReleased { get; set; }
        [Required]
        [Range(0.1, 10.0)]
        public decimal Rating { get; set; }

    }
}
