using Repository.Models.BaseEntities;

namespace Repository.Models;

public class User : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Changed { get; set; }
}