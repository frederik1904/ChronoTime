using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommonInterfaces.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services;

public class SecurityTokenValidator(IAppSettings appSettings) : TokenHandler
{
    
    public bool CanReadToken(string securityToken)
    {
        return true;
    }
    public bool CanValidateToken { get; } = true;
    public int MaximumTokenSizeInBytes { get; set; } = int.MaxValue;
    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
        out SecurityToken validatedToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSettings.GetSecret().ExposeSecret());

        var claimsPrincipal = tokenHandler.ValidateToken(securityToken, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateActor = false,
            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            ClockSkew = TimeSpan.Zero
        }, out validatedToken);

        return claimsPrincipal;
    }
}