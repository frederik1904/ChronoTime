using CommonModels.Wrappers;

namespace CommonInterfaces.Configuration;

public interface IAppSettings
{
    public Secret<string> GetSecret();
    public int GetAuthExpiry();

    public Secret<string> GetConnectionString();
}