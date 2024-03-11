using CommonInterfaces.Services;
using Grpc.Core;
using Repository.Repositories;
using WorkflowApplication;
using WorkflowCore.Interface;

namespace WorkflowApplication.Services;

public class GreeterService(ILogger<GreeterService> logger, IWorkflowHost workflowHost) : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        workflowHost.StartWorkflow("StartStopRegisterTimeFlow");
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}