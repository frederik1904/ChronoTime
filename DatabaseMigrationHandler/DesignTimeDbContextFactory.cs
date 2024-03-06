using CommonInterfaces.Configuration;
using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repository;

namespace DatabaseMigrationHandler;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ChronoContext>
{
    public ChronoContext CreateDbContext(string[] args)
    {
        ServiceProvider serviceProvider;
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("appsettings.json", true)
            .Build();

        services.AddOptions();
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.AddSingleton<IAppSettings, AppSettingsSingleton>();
        serviceProvider = services.BuildServiceProvider();

        return new ChronoContext(serviceProvider.GetService<IAppSettings>() ?? throw new InvalidOperationException());
    }
}