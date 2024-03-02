using Common;
using CommonInterfaces.Configuration;
using Configuration;
using Repository;

namespace ApiApplication;

public static class ServicesSetup
{
    public static void Setup(WebApplicationBuilder builder)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)))
            .AddDbContext<ChronoContext>()
            .AddSingleton<IAppSettings, AppSettings>()
            .AddControllers();
        
        // Setup Repositories
        RepositoryDependencyInjectionSetup.Setup(builder.Services);
        
        //Setup Common
        CommonDependencyInjectionSetup.Setup(builder.Services);
    }
}