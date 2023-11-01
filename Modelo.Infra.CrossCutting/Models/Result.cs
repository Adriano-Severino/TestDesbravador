using Modelo.Domain.Entities;
using Modelo.Infra.CrossCutting.Enums;
using Newtonsoft.Json;

namespace Modelo.Infra.CrossCutting.Models
{
    public partial class Result
    {
        [JsonProperty("gender")]
        public GenderEnum Gender { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("login")]
        public Login Login { get; set; }

        [JsonProperty("dob")]
        public Dob Dob { get; set; }

        [JsonProperty("registered")]
        public Dob Registered { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("cell")]
        public string Cell { get; set; }

        [JsonProperty("id")]
        public Id Id { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }

        [JsonProperty("nat")]
        public string Nat { get; set; }
    }
}
