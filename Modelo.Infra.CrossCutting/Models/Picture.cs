using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Picture
    {
        [JsonProperty("large")]
        public Uri Large { get; set; }

        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }
    }
}
