using Common.Services;
using CommonInterfaces.Configuration;
using CommonInterfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class CommonDependencyInjectionSetup
{
    public static void Setup(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserServiceImpl>();
    }
}