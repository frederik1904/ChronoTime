using CommonInterfaces.Models.Authentication;
using WorkflowApplication.BaseWorkflow;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Models;

public class ContextData : BaseWorkflowContext
{
    public DateTime ST { get; set; }
    public DateTime ET { get; set; }
}