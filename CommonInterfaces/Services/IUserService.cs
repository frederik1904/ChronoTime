using Common.Models.Validation;
using CommonInterfaces.Models;
using CommonInterfaces.Services.Authentication;
using CommonInterfaces.Wrappers;

namespace CommonInterfaces.Services;

public interface IUserService : ISecurityUserService
{
    IEnumerable<Guid> GetAllUserIds();

    AUser? GetUserByEmail(string email);

    bool CheckIfPasswordsMatchAndUpgradeIfNeeded(AUser aUser, Secret<string> password);

    AUser? RegisterUser(ValidatedUserApplicant validatedApplicantValidatedObject);
}