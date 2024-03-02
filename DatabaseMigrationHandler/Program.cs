// See https://aka.ms/new-console-template for more information


using CommonInterfaces.Configuration;
using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

IServiceProvider? serviceProvider;
var baseDirectory = Directory.GetCurrentDirectory();


void RegisterServices()
{
    var services = new ServiceCollection();
    var configuration = new ConfigurationBuilder()
        .SetBasePath(baseDirectory)
        .AddJsonFile("appsettings.Development.json")
        .AddJsonFile("appsettings.json", true)
        .Build();   

    services.AddOptions();   
    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
    services.AddScoped<IAppSettings, AppSettings>();
    services.AddDbContext<ChronoContext>();
    serviceProvider = services.BuildServiceProvider();
}

void DisposeServices()
{
    switch (serviceProvider)
    {
        case null:
            return;
        case IDisposable disposable:
            disposable.Dispose();
            break;
    }
}

RegisterServices();

DisposeServices();

