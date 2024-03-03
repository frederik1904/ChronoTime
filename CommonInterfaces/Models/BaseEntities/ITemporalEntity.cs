namespace CommonInterfaces.Models.BaseEntities;

public interface ITemporalEntity
{
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}