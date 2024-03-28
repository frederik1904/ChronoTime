namespace CommonInterfaces.Models.Authentication;

public class ApplicationContext
{
    public string? TenantId { get; set; }
    public Guid? UserId { get; set; }
}