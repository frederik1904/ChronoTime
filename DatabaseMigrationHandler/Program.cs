// See https://aka.ms/new-console-template for more information


using Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

IServiceProvider? serviceProvider;
string baseDirectory = Directory.GetCurrentDirectory();

Console.WriteLine(baseDirectory);
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

using (var context = serviceProvider.GetService<ChronoContext>())
{
    Console.WriteLine(context.Users.Any());
}

DisposeServices();

