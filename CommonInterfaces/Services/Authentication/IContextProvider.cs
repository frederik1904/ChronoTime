using CommonInterfaces.Models.Authentication;

namespace CommonInterfaces.Services.Authentication;

public interface IContextProvider
{
    ApplicationContext SetApplicationContext(ApplicationContext applicationContext);
    ApplicationContext GetApplicationContext();
}