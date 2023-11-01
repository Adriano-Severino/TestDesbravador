using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Id
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
