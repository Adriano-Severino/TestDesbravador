using Modelo.Infra.CrossCutting.Enums;
using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Name
    {
        [JsonProperty("title")]
        public TitleEnum Title { get; set; }

        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
}
