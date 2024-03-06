using Repository.Models.TimeManagement;

namespace Repository.Repositories;

public class TopicRepository(ChronoContext chronoContext) : BaseRepository<Topic>(chronoContext)
{
    
};