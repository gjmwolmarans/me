using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace me.shared.Converters;

public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var split = reader.GetString().Split(",")[0].Split(":").Select(s => int.Parse(s))
            .ToArray();
        return new TimeSpan(split[0], split[1], split[2], split[3]);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("G"));
    }
}
