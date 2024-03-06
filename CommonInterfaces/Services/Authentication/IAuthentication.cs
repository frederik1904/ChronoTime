using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Wrappers;

namespace CommonInterfaces.Services.Authentication;

public interface IAuthentication
{
    string GenerateJwtToken(ISecurityUser? securityUser);

    PasswordSalt ComputeHashAndSalt(Secret<string> password);

    bool ComparePasswords(ISecurityUser user, Secret<string> password);

    HashAlgorithmType GetCurrentAlgorithmType();
}