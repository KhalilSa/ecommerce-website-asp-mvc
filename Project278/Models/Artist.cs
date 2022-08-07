using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Project278.Models
{
    public class Artist
    {
        [Key]
        [Required]
        [JsonProperty("id")]
        public int ArtistId { get; set; }
        [Required]
        [JsonProperty("title")]
        public string ArtistName { get; set; }
        [JsonProperty("thumb")]
        public string? ImgUrl { get; set; }
        public HashSet<Label>? Labels { get; set; }
        public List<ArtistBand>? ArtistBands { get; set; }
        
    }
}
