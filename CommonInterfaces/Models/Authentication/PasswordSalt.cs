namespace CommonInterfaces.Models.Authentication;

public class PasswordSalt
{
    public PasswordSalt(byte[]? salt, byte[] password, HashAlgorithmType hashAlgorithmType)
    {
        Salt = salt;
        Password = password;
        HashAlgorithmType = hashAlgorithmType;
    }

    public byte[]? Salt { get; set; }
    public byte[] Password { get; set; }
    public HashAlgorithmType HashAlgorithmType { get; set; }
}