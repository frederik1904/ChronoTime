using System.Diagnostics;
using Authentication;
using Authentication.Helpers;
using Authentication.Services;
using Common;
using CommonInterfaces.Configuration;
using Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Repository;
using WorkflowApplication.BaseWorkflow;
using WorkflowApplication.Services;
using WorkflowApplication.Workflows.StartStopRegisterTime;
using WorkflowApplication.Workflows.StartStopRegisterTime.Listners;
using WorkflowApplication.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Get the types of all classes that derive from StepBody
var stepBodyTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(BaseWorkflowStepBody)));

// Register all of the found types as Transient services
foreach (var type in stepBodyTypes)
{
    builder.Services.AddTransient(type);
}

builder.Services.AddGrpc()
    .Services
    .AddWorkflow()
    .Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)))
    .AddSingleton<IAppSettings, AppSettingsSingleton>()
    .AddDbContext<ChronoContext>()
    .AddTransient<Persist>()
    .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, CustomJwtBearerOptionsPostConfigureOptions>()
    .AddSingleton<SecurityTokenValidator>()
    .AddSingleton<IAuthorizationHandler, TestRequirementHandler>()
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


var workflow = app.Services.GetService<IWorkflowHost>();
Debug.Assert(workflow != null, nameof(workflow) + " != null");
workflow.RegisterWorkflow<StartStopRegisterTimeFlow, ContextData>();
workflow.Start();

app.Run();
workflow.Stop();