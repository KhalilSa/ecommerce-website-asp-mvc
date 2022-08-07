using Newtonsoft.Json;

namespace Project278.Models
{
    public class ArtistJson
    {
        [JsonProperty("results")]
        public List<Artist> Artists { get; set; }
    }
}
