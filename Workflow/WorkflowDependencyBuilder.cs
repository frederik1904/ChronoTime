using Microsoft.Extensions.DependencyInjection;
using Workflow.Base;

namespace Workflow;

public class WorkflowDependencyBuilder
{
    private Dictionary<Type, Type> _dependencies = new();


    public WorkflowDependencyBuilder AddDependency(Type interfaceType, Type implementationType)
    {
        _dependencies.Add(interfaceType, implementationType);
        return this;
    }

    public WorkflowDependencyBuilder AddDependency(Type implementationType)
    {
        return AddDependency(implementationType, implementationType);
    }

    public void Build(IServiceCollection services)
    {
        // Get the types of all classes that derive from StepBody
        var stepBodyTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(BaseWorkflowStepBody)));

        // Register all of the found types as Transient services
        foreach (var type in stepBodyTypes)
        {
            services.AddTransient(type);
        }
        
        foreach (var (interfaceType, implementationType) in _dependencies)
            services.AddScoped(interfaceType, implementationType);
    }
}