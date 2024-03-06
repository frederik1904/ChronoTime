using CommonInterfaces.Models.TimeManagement;

namespace Repository.Models.TimeManagement;

public class TimeRegistration : ATimeRegistration
{
    public virtual Topic? Topic { get; set; }
    public virtual User User { get; set; }
}