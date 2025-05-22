using Newtonsoft.Json;
using System.Globalization;

namespace API.Core.Utils
{
    public class TimeSpanNewtonsoftConverter : JsonConverter<TimeSpan>
    {
        private const string FormatString = @"hh\:mm\:ss";

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(FormatString));
        }

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return TimeSpan.ParseExact((string)reader.Value, FormatString, CultureInfo.InvariantCulture);
        }
    }
}
