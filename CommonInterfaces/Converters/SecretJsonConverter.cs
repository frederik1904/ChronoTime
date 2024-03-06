using System.Text.Json;
using System.Text.Json.Serialization;
using CommonInterfaces.Wrappers;

namespace CommonModels.Converters;

public class SecretJsonConverter<T> : JsonConverter<Secret<T>>
{
    public override Secret<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeof(T) != typeof(string)) throw new Exception();
        if (reader.TokenType != JsonTokenType.String) throw new Exception();

        var input = reader.GetString();
        return input != null ? new Secret<T>((T)Convert.ChangeType(input, typeof(T))) : null;
    }

    public override void Write(Utf8JsonWriter writer, Secret<T> value, JsonSerializerOptions options)
    {
        //Nothing is written, its a secret after all
    }
}