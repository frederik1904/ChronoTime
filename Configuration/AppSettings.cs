
using CommonInterfaces.Configuration;
using CommonModels.Wrappers;

namespace Configuration;

public class AppSettings
{
    public required Secret<string> Secret { get; set; }
    public int AuthExpiry { get; set; }

    public required Secret<string> ConnectionString { get; set; }
}