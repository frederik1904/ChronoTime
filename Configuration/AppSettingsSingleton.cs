using CommonInterfaces.Configuration;
using CommonInterfaces.Wrappers;
using Microsoft.Extensions.Options;

namespace Configuration;

public class AppSettingsSingleton(IOptions<AppSettings> appSettings) : IAppSettings
{
    private AppSettings _appSettings = appSettings.Value;

    public Secret<string> GetSecret()
    {
        return _appSettings.Secret;
    }

    public int GetAuthExpiry()
    {
        return _appSettings.AuthExpiry;
    }

    public Secret<string> GetConnectionString()
    {
        return _appSettings.ConnectionString;
    }
}