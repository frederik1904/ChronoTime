using System.Diagnostics;
using Authentication;
using Common;
using CommonInterfaces.Configuration;
using Configuration;
using Repository;
using WorkflowApplication.Services;
using WorkflowApplication.Workflows.StartStopRegisterTime;
using WorkflowApplication.Workflows.StartStopRegisterTime.Listners;
using WorkflowApplication.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc()
    .Services
    .AddWorkflow()
    .Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)))
    .AddSingleton<IAppSettings, AppSettingsSingleton>()
    .AddDbContext<ChronoContext>()
    .AddTransient<Persist>();
new RepositoryDependencyBuilder()
    .Build(builder.Services);

new CommonDependencyBuilder()
    .Build(builder.Services);

new AuthenticationDependencyInjectionBuilder()
    .Build(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
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