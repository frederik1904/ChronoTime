using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Wrappers;

namespace Authentication.Services.HashingAlgorithms;

public interface IHashAlgorithm
{
    private static readonly Dictionary<HashAlgorithmType, IHashAlgorithm> Map =
        new()
        {
            { HashAlgorithmType.Bcrypt, new HashAlgorithmBcrypt() },
            { HashAlgorithmType.Sha256, new HashAlgorithmSha256() }
        };

    public PasswordSalt ComputeHashAndSalt(Secret<string> password);

    public bool ValidatePassword(PasswordSalt passwordSalt, Secret<string> password);

    public static IHashAlgorithm GetImplementation(HashAlgorithmType type)
    {
        return Map[type];
    }
}