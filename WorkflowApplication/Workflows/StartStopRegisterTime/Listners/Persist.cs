using CommonInterfaces.Models.Database;
using CommonInterfaces.Models.Database.TimeManagement;
using CommonInterfaces.Services;
using Repository.Repositories;
using WorkflowApplication.Workflows.StartStopRegisterTime.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowApplication.Workflows.StartStopRegisterTime.Listners;

public class Persist : StepBody
{
    private readonly TimeRegistrationRepository _timeRegistrationRepository;
    private readonly UserRepository _userRepository;
    private readonly ITransactionService _transactionService;

    public Persist(TimeRegistrationRepository timeRegistrationRepository, UserRepository userRepository, ITransactionService transactionService)
    {
        _timeRegistrationRepository = timeRegistrationRepository;
        _userRepository = userRepository;
        _transactionService = transactionService;
    }

    public DateTime StartTime { get; set; }
    public DateTime StopTime { get; set; }
    
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        Console.WriteLine($"Persisting: {StartTime} - {StopTime}");
        _transactionService.Transactional(() =>
        {
            User user = _userRepository.GetByEmail("frederik1904@gmail.com");
            TimeRegistration tr = new TimeRegistration();
            tr.User = user;
            tr.ValidFrom = StartTime.ToUniversalTime();
            tr.ValidTo = StopTime.ToUniversalTime();
            return _timeRegistrationRepository.Save(tr);
        });
        
        return ExecutionResult.Next();
    }
}