using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Workflows.StartStopRegisterTime;
using Workflow.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;

namespace Workflow;

public class WorkflowPostBuildConfiguration
{
    
    public static IWorkflowHost? WorkflowHost { get; set; }
    
    public static void Pre(IServiceProvider services)
    {
        WorkflowHost = services.GetService<IWorkflowHost>();
        Debug.Assert(WorkflowHost != null, nameof(WorkflowHost) + " != null");
        WorkflowHost.RegisterWorkflow<StartStopRegisterTimeFlow, ContextData>();
        WorkflowHost.Start();
    }

    public static void Post()
    {
        WorkflowHost.Stop();   
    }
}