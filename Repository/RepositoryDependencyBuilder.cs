using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace Repository;

public class RepositoryDependencyBuilder
{
    private Dictionary<Type, Type> _dependencies = new()
    {
        { typeof(UserRepository), typeof(UserRepository) },
        { typeof(TimeRegistrationRepository), typeof(TimeRegistrationRepository) },
        { typeof(TopicRepository), typeof(TopicRepository) }
    };


    public RepositoryDependencyBuilder AddDependency(Type interfaceType, Type implementationType)
    {
        _dependencies.Add(interfaceType, implementationType);
        return this;
    }

    public RepositoryDependencyBuilder AddDependency(Type implementationType)
    {
        return AddDependency(implementationType, implementationType);
    }

    public void Build(IServiceCollection services)
    {
        foreach (var (interfaceType, implementationType) in _dependencies)
            services.AddScoped(interfaceType, implementationType);
    }
}