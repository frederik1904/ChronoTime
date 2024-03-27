using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Services.Authentication;
using WorkflowApplication.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.MiddleWares;

public class PreContextMiddleware(IContextProvider contextProvider) : IWorkflowStepMiddleware
{
    public async Task<ExecutionResult> HandleAsync(IStepExecutionContext context, IStepBody body, WorkflowStepDelegate next)
    {
        if (context.Workflow.Data is ContextData ctx)
        {
            contextProvider.SetApplicationContext(ctx.ApplicationContext);
        }

        return await next();    
    }
}