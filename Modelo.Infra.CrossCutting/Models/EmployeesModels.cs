using System.Globalization;
using Modelo.Infra.CrossCutting.Enums;
using Modelo.Infra.CrossCutting.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Modelo.Domain.Entities
{
    public partial class EmployeesModels
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                GenderConverter.Singleton,
                PostcodeConverter.Singleton,
                TitleConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GenderConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GenderEnum) || t == typeof(GenderEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "female":
                    return GenderEnum.Female;
                case "male":
                    return GenderEnum.Male;
            }
            throw new Exception("Cannot unmarshal type Gender");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GenderEnum)untypedValue;
            switch (value)
            {
                case GenderEnum.Female:
                    serializer.Serialize(writer, "female");
                    return;
                case GenderEnum.Male:
                    serializer.Serialize(writer, "male");
                    return;
            }
            throw new Exception("Cannot marshal type Gender");
        }

        public static readonly GenderConverter Singleton = new GenderConverter();
    }

    internal class PostcodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Postcode) || t == typeof(Postcode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Postcode { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Postcode { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type Postcode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Postcode)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            throw new Exception("Cannot marshal type Postcode");
        }

        public static readonly PostcodeConverter Singleton = new PostcodeConverter();
    }

    internal class TitleConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TitleEnum) || t == typeof(TitleEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Miss":
                    return TitleEnum.Miss;
                case "Monsieur":
                    return TitleEnum.Monsieur;
                case "Mr":
                    return TitleEnum.Mr;
                case "Mrs":
                    return TitleEnum.Mrs;
                case "Ms":
                    return TitleEnum.Ms;
                case "Mademoiselle":
                    return TitleEnum.Mademoiselle;
                case "Madame":
                    return TitleEnum.Madame;
            }
            throw new Exception("Cannot unmarshal type Title");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TitleEnum)untypedValue;
            switch (value)
            {
                case TitleEnum.Miss:
                    serializer.Serialize(writer, "Miss");
                    return;
                case TitleEnum.Monsieur:
                    serializer.Serialize(writer, "Monsieur");
                    return;
                case TitleEnum.Mr:
                    serializer.Serialize(writer, "Mr");
                    return;
                case TitleEnum.Mrs:
                    serializer.Serialize(writer, "Mrs");
                    return;
                case TitleEnum.Ms:
                    serializer.Serialize(writer, "Ms");
                    return;
                case TitleEnum.Mademoiselle:
                    serializer.Serialize(writer, "Mademoiselle");
                    return;
                case TitleEnum.Madame:
                    serializer.Serialize(writer, "Madame");
                    return;
            }
            throw new Exception("Cannot marshal type Title");
        }

        public static readonly TitleConverter Singleton = new TitleConverter();
    }
}
