using CommonInterfaces.Services.Authentication;
using Workflow.Base;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Listners;

public class Stop(IContextProvider contextProvider) : BaseWorkflowStepBody(contextProvider)
{
    public DateTime StopTime { get; set; }

    protected override ExecutionResult MiddlewareRun(IStepExecutionContext context)
    {
        Console.WriteLine("Stop");
        StopTime = DateTime.Now;
        return ExecutionResult.Next();    }
}