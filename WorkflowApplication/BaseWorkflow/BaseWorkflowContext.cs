using CommonInterfaces.Models.Authentication;

namespace WorkflowApplication.BaseWorkflow;

public abstract class BaseWorkflowContext
{
    public ApplicationContext ApplicationContext { get; set; }

}