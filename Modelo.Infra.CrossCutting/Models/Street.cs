using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Street
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
