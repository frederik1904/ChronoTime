using CommonInterfaces.Models.Authentication;

namespace Workflow.Base;

public abstract class BaseWorkflowContext
{
    public ApplicationContext ApplicationContext { get; set; }

}