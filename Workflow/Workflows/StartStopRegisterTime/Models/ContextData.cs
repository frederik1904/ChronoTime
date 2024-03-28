using Workflow.Base;

namespace Workflow.Workflows.StartStopRegisterTime.Models;

public class ContextData : BaseWorkflowContext
{
    public DateTime ST { get; set; }
    public DateTime ET { get; set; }
}