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
            .AddSingleton<IAppSettings, AppSettingsSingleton>()
            .AddDbContext<ChronoContext>()
            .AddControllers();
        
        // Setup Repositories
        new RepositoryDependencyBuilder()
            .Build(builder.Services);
        
        //Setup Common
        new CommonDependencyBuilder()
            .Build(builder.Services);
    }
}