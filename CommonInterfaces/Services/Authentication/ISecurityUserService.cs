using CommonInterfaces.Models;
using CommonInterfaces.Models.Authentication;

namespace CommonInterfaces.Services.Authentication;

public interface ISecurityUserService
{
    IUser? GetById(Guid guid);
}