namespace CommonInterfaces.Services;

public interface IUserService
{
    IEnumerable<Guid> GetAllUserIds();
}