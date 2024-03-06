using Repository.Models.TimeManagement;

namespace Repository.Repositories;

public class TimeRegistrationRepository(ChronoContext chronoContext) : BaseRepository<TimeRegistration>(chronoContext)
{
    
};