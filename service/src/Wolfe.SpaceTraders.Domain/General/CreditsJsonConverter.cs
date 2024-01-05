using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wolfe.SpaceTraders.Domain.General;

internal class CreditsJsonConverter : JsonConverter<Credits>
{
    public override Credits Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new(reader.GetInt64());

    public override void Write(Utf8JsonWriter writer, Credits value, JsonSerializerOptions options) => writer.WriteNumberValue(value.Value);

    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(long) || typeToConvert == typeof(int) || typeToConvert == typeof(short) || base.CanConvert(typeToConvert);
}