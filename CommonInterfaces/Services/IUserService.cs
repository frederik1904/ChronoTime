using Common.Models.Validation;
using CommonInterfaces.Models;
using CommonInterfaces.Models.Database;
using CommonInterfaces.Services.Authentication;
using CommonInterfaces.Wrappers;

namespace CommonInterfaces.Services;

public interface IUserService : ISecurityUserService
{
    IEnumerable<Guid> GetAllUserIds();

    User? GetUserByEmail(string email);

    bool CheckIfPasswordsMatchAndUpgradeIfNeeded(User user, Secret<string> password);

    User? RegisterUser(ValidatedUserApplicant validatedApplicantValidatedObject);
}