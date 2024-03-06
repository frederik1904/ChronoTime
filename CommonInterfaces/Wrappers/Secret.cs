using System.Text.Json.Serialization;

namespace CommonInterfaces.Wrappers;

public class Secret<T>(T secretValue)
{
    [JsonIgnore] private T SecretValue { get; } = secretValue;

    public T ExposeSecret()
    {
        return SecretValue;
    }

    public override string ToString()
    {
        return "<REDACTED>";
    }
}