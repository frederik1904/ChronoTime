using CommonInterfaces.Configuration;
using CommonInterfaces.Wrappers;

namespace Configuration;

public class AppSettings
{
    public required Secret<string> Secret { get; set; }
    public int AuthExpiry { get; set; }

    public required Secret<string> ConnectionString { get; set; }
}