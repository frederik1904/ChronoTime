using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Models.BaseEntities;

namespace CommonInterfaces.Models;

public abstract class AUser : IBaseEntity, ISecurityUser
{
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[]? Salt { get; set; }
    public byte[] Password { get; set; }
    public HashAlgorithmType HashAlgorithmType { get; set; }
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Changed { get; set; }
    public Guid GetId()
    {
        return Id;
    }

    public HashAlgorithmType GetAlgorithmType()
    {
        return HashAlgorithmType;
    }

    public PasswordSalt GetUserPasswordSalt()
    {
        return new PasswordSalt(Salt, Password, HashAlgorithmType);
    }

    public Guid GetTenantId()
    {
        throw new NotImplementedException();
    }
}