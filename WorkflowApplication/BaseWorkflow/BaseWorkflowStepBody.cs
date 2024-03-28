using CommonInterfaces.Services.Authentication;
using WorkflowApplication.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.BaseWorkflow;

public abstract class BaseWorkflowStepBody(IContextProvider contextProvider) : StepBody
{
    public static readonly List<ExecutionResult> _ContinueResults = new List<ExecutionResult>()
    {
        ExecutionResult.Next(),
    };
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        PreMiddleware(context);

        var result = MiddlewareRun(context);

        PostMiddleware(result, context);

        return result;
    }

    //Pre middleware
    protected virtual void PreMiddleware(IStepExecutionContext context)
    {
        if (context.Workflow.Data is ContextData ctx)
        {
            contextProvider.SetApplicationContext(ctx.ApplicationContext);
        }
    }

    //Main execution of the step
    protected abstract ExecutionResult MiddlewareRun(IStepExecutionContext context);

    //Post middleware
    protected virtual void PostMiddleware(ExecutionResult result, IStepExecutionContext context)
    {
        if (result.Proceed) return;
        
        if (context.Workflow.Data is BaseWorkflowContext ctx)
        {
            ctx.ApplicationContext = null;
        }
    }
}