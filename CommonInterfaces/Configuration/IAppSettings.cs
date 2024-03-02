using CommonModels.Wrappers;

namespace CommonInterfaces.Configuration;

public interface IAppSettings
{
    public Secret<string> Secret { get; set; }
    public int AuthExpiry { get; set; }

    public Secret<string> ConnectionString { get; set; }
}