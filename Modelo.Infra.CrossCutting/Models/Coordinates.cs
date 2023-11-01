using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Coordinates
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }
}
