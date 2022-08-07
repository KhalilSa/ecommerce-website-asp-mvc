using Newtonsoft.Json;

namespace Project278.Models
{
    public class LabelJson
    {
        [JsonProperty("releases")]
        public List<Label> Labels { get; set; }
    }
}
