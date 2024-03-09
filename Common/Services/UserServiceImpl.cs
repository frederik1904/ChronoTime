using Common.Models.Validation;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using CommonInterfaces.Wrappers;
using Repository.Repositories;
using User = CommonInterfaces.Models.Database.User;

namespace Common.Services;

public class UserServiceImpl(UserRepository userRepository, IAuthentication authentication) : IUserService
{
    public IEnumerable<Guid> GetAllUserIds()
    {
        return userRepository.GetAll().Select(user => user.Id).ToList();
    }

    public User? GetUserByEmail(string email)
    {
        return userRepository.GetByEmail(email);
    }

    public bool CheckIfPasswordsMatchAndUpgradeIfNeeded(User user, Secret<string> password)
    {
        var doesMatch = authentication.ComparePasswords(user, password);

        if (doesMatch && user.HashAlgorithmType != authentication.GetCurrentAlgorithmType())
            UpdatePasswordForUser(password, user);

        return doesMatch;
    }

    public User? RegisterUser(ValidatedUserApplicant validatedUser)
    {
        var user = new User()
        {
            Email = validatedUser.Email.Email,
            Username = validatedUser.Username.Username,
            Id = Guid.NewGuid()
        };
        UpdatePasswordForUser(validatedUser.Password.Password, user);

        return userRepository.Save(user);
    }

    public User? GetById(Guid guid)
    {
        return userRepository.GetById(guid);
    }

    private void UpdatePasswordForUser(Secret<string> password, User user)
    {
        var passwordAndSalt = authentication.ComputeHashAndSalt(password);
        user.Password = passwordAndSalt.Password;
        user.Salt = passwordAndSalt.Salt;
        user.HashAlgorithmType = passwordAndSalt.HashAlgorithmType;
    }
}