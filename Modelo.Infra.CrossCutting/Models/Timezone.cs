using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Timezone
    {
        [JsonProperty("offset")]
        public string Offset { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
