namespace CommonInterfaces.Models.BaseEntities;

public interface IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Changed { get; set; }
}