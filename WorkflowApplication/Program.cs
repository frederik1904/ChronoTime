using Authentication;
using Authentication.Helpers;
using Authentication.Services;
using Common;
using CommonInterfaces.Configuration;
using Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Repository;
using Workflow;
using WorkflowApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc()
    .Services
    .AddWorkflow()
    .Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)))
    .AddSingleton<IAppSettings, AppSettingsSingleton>()
    .AddDbContext<ChronoContext>()
    .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, CustomJwtBearerOptionsPostConfigureOptions>()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Test", policy =>
        {
            policy.RequireAuthenticatedUser();
            
            policy.Requirements.Add(new TestRequirement());
        });
    });
new RepositoryDependencyBuilder()
    .Build(builder.Services);

new CommonDependencyBuilder()
    .Build(builder.Services);

new AuthenticationDependencyInjectionBuilder()
    .Build(builder.Services);

new WorkflowDependencyBuilder()
    .Build(builder.Services);
    
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

WorkflowPostBuildConfiguration.Pre(app.Services);

app.Run();

WorkflowPostBuildConfiguration.Post();