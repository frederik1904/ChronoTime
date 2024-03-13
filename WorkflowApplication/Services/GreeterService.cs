using Authentication.Helpers;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using WorkflowCore.Interface;

namespace WorkflowApplication.Services;

public class GreeterService(ILogger<GreeterService> logger, IWorkflowHost workflowHost) : Greeter.GreeterBase
{
    [Authorize("Test")]
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        workflowHost.StartWorkflow("StartStopRegisterTimeFlow");
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}