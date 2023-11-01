using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Dob
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("age")]
        public long Age { get; set; }
    }
}
