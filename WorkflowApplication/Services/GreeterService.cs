using Authentication.Helpers;
using CommonInterfaces.Services.Authentication;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Workflow.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;

namespace WorkflowApplication.Services;

public class GreeterService(ILogger<GreeterService> logger, IWorkflowHost workflowHost, IContextProvider contextProvider) : Greeter.GreeterBase
{
    [Authorize("Test")]
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        var ctx = new ContextData();
        ctx.ApplicationContext = contextProvider.GetApplicationContext();
        workflowHost.StartWorkflow("StartStopRegisterTimeFlow", ctx);
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}