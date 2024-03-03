namespace CommonInterfaces.Models.Authentication;

public interface ISecurityUser
{
    Guid GetId();

    HashAlgorithmType GetAlgorithmType();

    PasswordSalt GetUserPasswordSalt();

    Guid GetTenantId();
}