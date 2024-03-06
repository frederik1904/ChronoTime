using System.Text;
using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Wrappers;

namespace Authentication.Services.HashingAlgorithms;

public class HashAlgorithmBcrypt : IHashAlgorithm
{
    private readonly int _strength;

    public HashAlgorithmBcrypt(int strength = 12)
    {
        _strength = strength;
    }

    private string GetRandomSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(_strength);
    }

    public PasswordSalt ComputeHashAndSalt(Secret<string> password)
    {
        return new PasswordSalt(
            null,
            Encoding.ASCII.GetBytes(BCrypt.Net.BCrypt.HashPassword(password.ExposeSecret(), GetRandomSalt())),
            HashAlgorithmType.Bcrypt
        );
    }

    public bool ValidatePassword(PasswordSalt passwordSalt, Secret<string> password)
    {
        return BCrypt.Net.BCrypt.Verify(
            password.ExposeSecret(),
            Encoding.ASCII.GetString(passwordSalt.Password)
        );
    }
}