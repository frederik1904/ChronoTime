using Common.Models.Validation;
using CommonInterfaces.Models;
using CommonInterfaces.Services.Authentication;
using CommonModels.Wrappers;

namespace CommonInterfaces.Services;

public interface IUserService : ISecurityUserService
{
    IEnumerable<Guid> GetAllUserIds();

    IUser? GetUserByEmail(String email);
    
    bool CheckIfPasswordsMatchAndUpgradeIfNeeded(IUser user, Secret<string> password);
    
    IUser? RegisterUser(ValidatedUserApplicant validatedApplicantValidatedObject);
}