using CommonInterfaces.Models.Database;
using CommonInterfaces.Models.Database.TimeManagement;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using Repository.Repositories;
using Workflow.Base;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Workflows.StartStopRegisterTime.Listners;

public class Persist(
    TimeRegistrationRepository timeRegistrationRepository,
    UserRepository userRepository,
    ITransactionService transactionService,
    IContextProvider contextProvider)
    : BaseWorkflowStepBody(contextProvider)
{
    private IContextProvider contextProvider = contextProvider;

    public DateTime StartTime { get; set; }
    public DateTime StopTime { get; set; }

    protected override ExecutionResult MiddlewareRun(IStepExecutionContext context)
    {
        Console.WriteLine($"Persisting: {StartTime} - {StopTime}");
        transactionService.Transactional(() =>
        {
            if (!contextProvider.GetApplicationContext().UserId.HasValue)
            {
                throw new Exception("No user is defined");
            }
            
            var userId = contextProvider.GetApplicationContext().UserId!.Value;
            User user = userRepository.GetById(userId);
            TimeRegistration tr = new TimeRegistration();
            tr.User = user;
            tr.ValidFrom = StartTime.ToUniversalTime();
            tr.ValidTo = StopTime.ToUniversalTime();
            return timeRegistrationRepository.Save(tr);
        });

        return ExecutionResult.Next();
    }
}