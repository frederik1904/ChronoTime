namespace Common.Models.Validation;

public class ValidatedUsername
{
    private ValidatedUsername(string username)
    {
        Username = username;
    }

    public string Username { get; }

    public static ValidationResponse<ValidatedUsername> CreateValidatedUsername(string username)
    {
        return new ValidationResponse<ValidatedUsername>(ValidationResponseType.Success,
            new ValidatedUsername(username));
    }
}