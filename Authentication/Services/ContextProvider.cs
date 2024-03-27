using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Services.Authentication;

namespace Authentication.Services;

public class ContextProvider : IContextProvider
{

    private ApplicationContext _context = new();
    
    public ApplicationContext SetApplicationContext(ApplicationContext applicationContext)
    {
        this._context = applicationContext;
        return GetApplicationContext();
    }

    public ApplicationContext GetApplicationContext()
    {
        return this._context;
    }
}