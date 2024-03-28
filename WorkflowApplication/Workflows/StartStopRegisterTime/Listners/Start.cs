using CommonInterfaces.Services.Authentication;
using WorkflowApplication.BaseWorkflow;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Listners;

public class Start(IContextProvider contextProvider) : BaseWorkflowStepBody(contextProvider)
{
    public DateTime StartTime { get; set; }

    protected override ExecutionResult MiddlewareRun(IStepExecutionContext context)
    {
        Console.WriteLine("Start");
        StartTime = DateTime.Now;
        return ExecutionResult.Next();    
    }
}