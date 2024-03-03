using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Services.HashingAlgorithms;
using CommonInterfaces.Configuration;
using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Services.Authentication;
using CommonModels.Wrappers;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Services;

public class JwtAuthentication(IAppSettings appSettings) : IAuthentication
{
    public string GenerateJwtToken(ISecurityUser? securityUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSettings.GetSecret().ExposeSecret());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", securityUser.GetId().ToString()) }),
            Expires = DateTime.UtcNow.AddDays(appSettings.GetAuthExpiry()),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public PasswordSalt ComputeHashAndSalt(Secret<string> password)
    {
        var hashAlgorithmType = GetCurrentAlgorithmType();
        var algorithm = IHashAlgorithm.GetImplementation(hashAlgorithmType);
        return algorithm.ComputeHashAndSalt(password);
    }

    public bool ComparePasswords(ISecurityUser user, Secret<string> password)
    {
        var passwordSalt = user.GetUserPasswordSalt();
        var algorithm = IHashAlgorithm.GetImplementation(passwordSalt.HashAlgorithmType);
        return algorithm.ValidatePassword(passwordSalt, password);
    }

    public HashAlgorithmType GetCurrentAlgorithmType()
    {
        return HashAlgorithmType.Bcrypt;
    }
}