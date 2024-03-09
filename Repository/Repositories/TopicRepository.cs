using CommonInterfaces.Models.Database.TimeManagement;

namespace Repository.Repositories;

public class TopicRepository(ChronoContext chronoContext) : BaseRepository<Topic>(chronoContext)
{
};