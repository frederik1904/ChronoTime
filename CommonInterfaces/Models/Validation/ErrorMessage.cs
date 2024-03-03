namespace Common.Models.Validation;

public class ErrorMessage(string message, string? field)
{
    public string Message { get; set; } = message;
    public string? Field { get; set; } = field;
}