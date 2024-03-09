using CommonInterfaces.Models.Database.TimeManagement;

namespace Repository.Repositories;

public class TimeRegistrationRepository(ChronoContext chronoContext) : BaseRepository<TimeRegistration>(chronoContext)
{
};