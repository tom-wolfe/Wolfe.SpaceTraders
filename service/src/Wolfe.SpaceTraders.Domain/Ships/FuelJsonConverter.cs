using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wolfe.SpaceTraders.Domain.Ships;

internal class FuelJsonConverter : JsonConverter<Fuel>
{
    public override Fuel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new(reader.GetInt32());

    public override void Write(Utf8JsonWriter writer, Fuel value, JsonSerializerOptions options) => writer.WriteNumberValue(value.Value);

    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(int) || base.CanConvert(typeToConvert);
}