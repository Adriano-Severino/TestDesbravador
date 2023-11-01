using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Info
    {
        [JsonProperty("seed")]
        public string Seed { get; set; }

        [JsonProperty("results")]
        public long Results { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
