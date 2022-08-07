using System.ComponentModel.DataAnnotations;

namespace Project278.Models
{
    public class Band
    {
        [Key]
        [Required]
        public int BandId { get; set; }
        [Required]
        public string BandName { get; set; }
        public string? Profile { get; set; }
        public string? ImgUrl { get; set; }
        public HashSet<Artist> Members { get; set; }
        public List<ArtistBand> ArtistBands { get; set; }
    }
}