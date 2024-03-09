using CommonInterfaces.Models;
using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Models.Database;

namespace CommonInterfaces.Services.Authentication;

public interface ISecurityUserService
{
    User? GetById(Guid guid);
}