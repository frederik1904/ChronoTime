using CommonInterfaces.Models.Authentication;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Models;

public class ContextData
{
    public DateTime ST { get; set; }
    public DateTime ET { get; set; }
    public ApplicationContext ApplicationContext { get; set; }
}