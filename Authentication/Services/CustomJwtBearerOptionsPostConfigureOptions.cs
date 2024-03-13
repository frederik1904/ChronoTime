using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Authentication.Services;

public class CustomJwtBearerOptionsPostConfigureOptions(SecurityTokenValidator securityTokenValidator) : IPostConfigureOptions<JwtBearerOptions>
{
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.TokenHandlers.Clear();
        options.TokenHandlers.Add(securityTokenValidator);
    }
}