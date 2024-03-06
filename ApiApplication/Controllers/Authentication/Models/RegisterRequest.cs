using System.Text.Json.Serialization;
using CommonInterfaces.Wrappers;
using CommonModels.Converters;

namespace ApiApplication.Controllers.Authentication.Models;

public class RegisterRequest
{
    public string Username { get; set; }
    public string Email { get; set; }

    [JsonConverter(typeof(SecretJsonConverter<string>))]
    public Secret<string> Password { get; set; }
}