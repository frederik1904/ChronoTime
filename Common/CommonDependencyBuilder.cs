using Common.Services;
using CommonInterfaces.Configuration;
using CommonInterfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public class CommonDependencyBuilder
{
    private Dictionary<Type, Type> _dependencies = new()
    {
        {typeof(IUserService), typeof(UserServiceImpl)}
    };


    public CommonDependencyBuilder AddDependency(Type interfaceType, Type implementationType)
    {
        _dependencies.Add(interfaceType, implementationType);
        return this;
    }

    public void Build(IServiceCollection services)
    {
        foreach (var (interfaceType, implementationType) in _dependencies)
        {
            services.AddScoped(interfaceType, implementationType);
        }
    }
}