using CommonInterfaces.Models.Database;
using CommonInterfaces.Models.Database.TimeManagement;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using Repository.Repositories;
using WorkflowApplication.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Listners;

public class Persist(
    TimeRegistrationRepository timeRegistrationRepository,
    UserRepository userRepository,
    ITransactionService transactionService,
    IContextProvider contextProvider)
    : StepBody
{
    public DateTime StartTime { get; set; }
    public DateTime StopTime { get; set; }

    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine($"Persisting: {StartTime} - {StopTime}");
        transactionService.Transactional(() =>
        {
            Console.WriteLine(contextProvider.GetApplicationContext()?.UserId ?? null);
            User user = userRepository.GetById(Guid.Parse(contextProvider.GetApplicationContext().UserId));
            TimeRegistration tr = new TimeRegistration();
            tr.User = user;
            tr.ValidFrom = StartTime.ToUniversalTime();
            tr.ValidTo = StopTime.ToUniversalTime();
            return timeRegistrationRepository.Save(tr);
        });

        return ExecutionResult.Next();
    }
}