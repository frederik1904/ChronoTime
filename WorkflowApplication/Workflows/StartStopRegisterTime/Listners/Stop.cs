using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Listners;

public class Stop : StepBody
{
    public DateTime StopTime { get; set; }
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("Stop");
        StopTime = DateTime.Now;
        return ExecutionResult.Next();
    }
}