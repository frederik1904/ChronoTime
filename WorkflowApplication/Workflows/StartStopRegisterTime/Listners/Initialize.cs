using CommonInterfaces.Services.Authentication;
using WorkflowApplication.BaseWorkflow;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Listners;

public class Initialize(IContextProvider contextProvider) : BaseWorkflowStepBody(contextProvider)
{
    protected override ExecutionResult MiddlewareRun(IStepExecutionContext context)
    {
        Console.WriteLine("Initialize");
        return ExecutionResult.Next();
    }
}