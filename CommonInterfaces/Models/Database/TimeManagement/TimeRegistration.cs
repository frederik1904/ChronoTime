using CommonInterfaces.Models.BaseEntities;

namespace CommonInterfaces.Models.Database.TimeManagement;

public class TimeRegistration : IBaseTemporalEntity
{
    public virtual Topic? Topic { get; set; }
    public virtual User User { get; set; }
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Changed { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}