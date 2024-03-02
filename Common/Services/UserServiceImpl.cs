using CommonInterfaces.Configuration;
using CommonInterfaces.Services;
using Repository.Repositories;

namespace Common.Services;

public class UserServiceImpl(UserRepository userRepository) : IUserService
{
    public IEnumerable<Guid> GetAllUserIds()
    {
        return userRepository.GetAll().Select(user => user.Id).ToList();
    }
}