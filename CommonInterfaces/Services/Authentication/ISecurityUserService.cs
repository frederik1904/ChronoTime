using CommonInterfaces.Models.Authentication;

namespace CommonInterfaces.Services.Authentication;

public interface ISecurityUserService
{
    ISecurityUser? GetById(Guid guid);
}