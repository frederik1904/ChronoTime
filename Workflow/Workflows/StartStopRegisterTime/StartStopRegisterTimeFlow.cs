using Workflow.Workflows.StartStopRegisterTime.Listners;
using Workflow.Workflows.StartStopRegisterTime.Models;
using WorkflowApplication.Workflows.StartStopRegisterTime.Listners;
using WorkflowCore.Interface;

namespace Workflow.Workflows.StartStopRegisterTime;

public class StartStopRegisterTimeFlow : IWorkflow<ContextData>
{

    public void Build(IWorkflowBuilder<ContextData> builder)
    {
        builder
            .StartWith<Initialize>()
            .Then<Start>()
            .Output(data => data.ST, data => data.StartTime)
            .Then<Stop>()
            .Output(data => data.ET, data => data.StopTime)
            .Then<Persist>()
            .Input(step => step.StartTime, data => data.ST)
            .Input(step => step.StopTime, data => data.ET);  
            ;
        
    }

    public string Id => "StartStopRegisterTimeFlow";
    public int Version => 1;
}