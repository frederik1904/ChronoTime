using System.Security.Cryptography;
using System.Text;
using CommonInterfaces.Models.Authentication;
using CommonModels.Wrappers;

namespace Authentication.Services.HashingAlgorithms;

public class HashAlgorithmSha256 : IHashAlgorithm
{
    public PasswordSalt ComputeHashAndSalt(Secret<string> password)
    {
        var salt = GenerateSalt();
        var passwordBytes = Encoding.ASCII.GetBytes(password.ExposeSecret());
        return new PasswordSalt(salt, GenerateSaltedHash(passwordBytes, salt), HashAlgorithmType.Sha256);
    }

    private byte[] GenerateSaltedHash(IReadOnlyList<byte> password, IReadOnlyList<byte> salt)
    {
        var plainTextWithSaltBytes = new byte[password.Count + salt.Count];

        for (var i = 0; i < password.Count; i++)
        {
            plainTextWithSaltBytes[i] = password[i];
        }

        for (var i = 0; i < salt.Count; i++)
        {
            plainTextWithSaltBytes[password.Count + i] = salt[i];
        }

        return SHA256.HashData(plainTextWithSaltBytes);
    }

    private byte[] GenerateSalt(int length = 32)
    {
        var result = new byte[length];
        new Random().NextBytes(result);
        return result;
    }

    public bool ValidatePassword(PasswordSalt passwordSalt, Secret<string> password)
    {
        var hashedPassword = GenerateSaltedHash(Encoding.ASCII.GetBytes(password.ExposeSecret()), passwordSalt.Salt
            ?? throw new InvalidOperationException("Salt cannot be null when SHA256 is used"));
        if (hashedPassword.Length != passwordSalt.Password.Length)
        {
            return false;
        }

        return !hashedPassword.Where((t, i) => passwordSalt.Password[i] != t).Any();
    }
}