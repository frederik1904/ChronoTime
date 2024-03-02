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
            .AddControllers();
    }
}