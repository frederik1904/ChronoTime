using System.Text.Json.Serialization;

namespace CommonModels.Wrappers;

public class Secret<T>
{
    public Secret(T secretValue)
    {
        SecretValue = secretValue;
    }

    [JsonIgnore] private T SecretValue { get; }

    public T ExposeSecret()
    {
        return SecretValue;
    }

    public override string ToString()
    {
        return "<REDACTED>";
    }
}