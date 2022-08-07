using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Project278.Models
{
    public class Label
    {
        [Key]
        [Required]
        [JsonProperty("id")]
        public int LabelId { get; set; }
        [JsonProperty("title")]
        [Required]
        public string Title { get; set; }
        public double Price { get; set; }
        [JsonProperty("thumb")]
        public string? Thumbnail { get; set; }
        [JsonProperty("format")]
        [Required]
        public string? Format { get; set; }
        [Display(Name = "Release Year")]
        [JsonProperty("year")]
        public int? ReleaseYear { get; set; }

        [JsonProperty("artist")]
        public string ArtistName { get; set; }

        [Required]
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }


    }
}
