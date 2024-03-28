using Authentication.Helpers;
using Authentication.Services;
using CommonInterfaces.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication;

public class AuthenticationDependencyInjectionBuilder
{
    private Dictionary<Type, Type> _scopedDependencies = new()
    {
        { typeof(IAuthentication), typeof(JwtAuthentication) },
        { typeof(IContextProvider), typeof(ContextProvider) }
    };

    private Dictionary<Type, Type> _singletonDependencies = new()
    {
        { typeof(SecurityTokenValidator), typeof(SecurityTokenValidator) },
        { typeof(IAuthorizationHandler), typeof(TestRequirementHandler) }
    };


    public AuthenticationDependencyInjectionBuilder AddScopedDependency(Type interfaceType, Type implementationType)
    {
        _scopedDependencies.Add(interfaceType, implementationType);
        return this;
    }
    

    public AuthenticationDependencyInjectionBuilder AddScopedDependency(Type implementationType)
    {
        return AddScopedDependency(implementationType, implementationType);
    }

    public AuthenticationDependencyInjectionBuilder AddSingletonDependency(Type interfaceType, Type implementationType)
    {
        _singletonDependencies.Add(interfaceType, implementationType);
        return this;
    }
    

    public AuthenticationDependencyInjectionBuilder AddSingletonDependency(Type implementationType)
    {
        return AddSingletonDependency(implementationType, implementationType);
    }
    
    public void Build(IServiceCollection services)
    {
        foreach (var (interfaceType, implementationType) in _scopedDependencies)
            services.AddScoped(interfaceType, implementationType);

        foreach (var (interfaceType, implementationType) in _singletonDependencies)
            services.AddSingleton(interfaceType, implementationType);
    }
}