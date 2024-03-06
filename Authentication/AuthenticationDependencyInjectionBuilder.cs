using Authentication.Services;
using CommonInterfaces.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication;

public class AuthenticationDependencyInjectionBuilder
{
    private Dictionary<Type, Type> _dependencies = new()
    {
        { typeof(IAuthentication), typeof(JwtAuthentication) }
    };


    public AuthenticationDependencyInjectionBuilder AddDependency(Type interfaceType, Type implementationType)
    {
        _dependencies.Add(interfaceType, implementationType);
        return this;
    }

    public AuthenticationDependencyInjectionBuilder AddDependency(Type implementationType)
    {
        return AddDependency(implementationType, implementationType);
    }

    public void Build(IServiceCollection services)
    {
        foreach (var (interfaceType, implementationType) in _dependencies)
            services.AddScoped(interfaceType, implementationType);
    }
}