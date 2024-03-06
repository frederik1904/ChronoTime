using Common.Models.Validation;
using CommonInterfaces.Configuration;
using CommonInterfaces.Models;
using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using CommonInterfaces.Wrappers;
using Repository.Models;
using Repository.Repositories;

namespace Common.Services;

public class UserServiceImpl(UserRepository userRepository, IAuthentication authentication) : IUserService
{
    public IEnumerable<Guid> GetAllUserIds()
    {
        return userRepository.GetAll().Select(user => user.Id).ToList();
    }

    public AUser? GetUserByEmail(string email)
    {
        return userRepository.GetByEmail(email);
    }

    public bool CheckIfPasswordsMatchAndUpgradeIfNeeded(AUser aUser, Secret<string> password)
    {
        var doesMatch = authentication.ComparePasswords(aUser, password);

        if (doesMatch && aUser.HashAlgorithmType != authentication.GetCurrentAlgorithmType())
            UpdatePasswordForUser(password, aUser);

        return doesMatch;
    }

    public AUser? RegisterUser(ValidatedUserApplicant validatedUser)
    {
        var user = new Repository.Models.User()
        {
            Email = validatedUser.Email.Email,
            Username = validatedUser.Username.Username,
            Id = Guid.NewGuid()
        };
        UpdatePasswordForUser(validatedUser.Password.Password, user);

        return userRepository.Save(user);
    }

    public AUser? GetById(Guid guid)
    {
        return userRepository.GetById(guid);
    }

    private void UpdatePasswordForUser(Secret<string> password, AUser aUser)
    {
        var passwordAndSalt = authentication.ComputeHashAndSalt(password);
        aUser.Password = passwordAndSalt.Password;
        aUser.Salt = passwordAndSalt.Salt;
        aUser.HashAlgorithmType = passwordAndSalt.HashAlgorithmType;
    }
}