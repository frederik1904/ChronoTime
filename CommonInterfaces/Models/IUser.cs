using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Models.BaseEntities;

namespace CommonInterfaces.Models;

public interface IUser : IBaseEntity, ISecurityUser
{
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[]? Salt { get; set; }
    public byte[] Password { get; set; }
    public HashAlgorithmType HashAlgorithmType { get; set; }
}