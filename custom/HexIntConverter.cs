using System;
using Newtonsoft.Json;

public class HexIntConverter : JsonConverter<int>
{
    public override int ReadJson(
        JsonReader reader,
        Type objectType,
        int existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        string s = (string)reader.Value;

        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            s = s.Substring(2);

        return Convert.ToInt32(s, 16);
    }

    public override void WriteJson(
        JsonWriter writer,
        int value,
        JsonSerializer serializer)
    {
        writer.WriteValue($"0x{value:X}");
    }
}