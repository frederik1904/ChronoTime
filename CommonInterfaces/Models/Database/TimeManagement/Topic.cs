using CommonInterfaces.Models.BaseEntities;

namespace CommonInterfaces.Models.Database.TimeManagement;

public class Topic : IBaseEntity
{
    public string DisplayName { get; set; }
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Changed { get; set; }
}