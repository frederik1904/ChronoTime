using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Listners;

public class Start : StepBody
{
    public DateTime StartTime { get; set; }
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine("Start");
        StartTime = DateTime.Now;
        return ExecutionResult.Next();
    }
}