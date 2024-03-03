using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CommonModels.Converters;
using CommonModels.Wrappers;

namespace ApiApplication.Controllers.Authentication.Models;

public class LoginRequest
{
    [Required]
    [JsonConverter(typeof(SecretJsonConverter<string>))]
    public Secret<string> Password { get; set; }

    [Required] public string Email { get; set; }
}