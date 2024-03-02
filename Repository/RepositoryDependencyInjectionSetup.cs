using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace Repository;

public static class RepositoryDependencyInjectionSetup
{
    public static void Setup(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UserRepository>();
    }
}