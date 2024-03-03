using Repository.Models;

namespace Repository.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(ChronoContext chronoContext) : base(chronoContext)
    {
    }

    public IEnumerable<User> GetAll()
    {
        return ChronoContext.Users.ToList();
    }

    public User? GetByEmail(string email)
    {
        return ChronoContext.Users.FirstOrDefault(u => u.Email == email);
    }
}