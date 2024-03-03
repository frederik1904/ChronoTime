using Common.Models.Validation;
using CommonInterfaces.Configuration;
using CommonInterfaces.Models;
using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using CommonModels.Wrappers;
using Repository.Models;
using Repository.Repositories;

namespace Common.Services;

public class UserServiceImpl(UserRepository userRepository, IAuthentication authentication) : IUserService
{
    public IEnumerable<Guid> GetAllUserIds()
    {
        return userRepository.GetAll().Select(user => user.Id).ToList();
    }

    public IUser? GetUserByEmail(string email)
    {
        return userRepository.GetByEmail(email);
    }

    public bool CheckIfPasswordsMatchAndUpgradeIfNeeded(IUser user, Secret<string> password)
    {
        var doesMatch = authentication.ComparePasswords(user, password);

        if (doesMatch && user.HashAlgorithmType != authentication.GetCurrentAlgorithmType())
            UpdatePasswordForUser(password, user);

        return doesMatch;
    }

    public IUser? RegisterUser(ValidatedUserApplicant validatedUser)
    {
        var user = new User
        {
            Email = validatedUser.Email.Email,
            Username = validatedUser.Username.Username,
            Id = Guid.NewGuid()
        };
        UpdatePasswordForUser(validatedUser.Password.Password, user);

        return userRepository.Save(user);
    }

    public IUser? GetById(Guid guid)
    {
        return userRepository.GetById(guid);
    }
    
    private void UpdatePasswordForUser(Secret<string> password, IUser user)
    {
        var passwordAndSalt = authentication.ComputeHashAndSalt(password);
        user.Password = passwordAndSalt.Password;
        user.Salt = passwordAndSalt.Salt;
        user.HashAlgorithmType = passwordAndSalt.HashAlgorithmType;
    }
}