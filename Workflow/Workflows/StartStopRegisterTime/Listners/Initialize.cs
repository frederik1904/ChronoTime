using CommonInterfaces.Services.Authentication;
using Workflow.Base;
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