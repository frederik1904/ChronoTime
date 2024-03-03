namespace Common.Models.Validation;

public class ValidatedUserApplicant
{
    private ValidatedUserApplicant(ValidatedEmail email, ValidatedUsername username, ValidatedPassword password)
    {
        Email = email;
        Username = username;
        Password = password;
    }

    public ValidatedEmail Email { get; }
    public ValidatedUsername Username { get; }
    public ValidatedPassword Password { get; }

    public static ValidationResponse<ValidatedUserApplicant> CreateValidatedApplicant(string email, string username,
        string password)
    {
        var emailResponse = ValidatedEmail.CreateValidatedEmail(email);
        var usernameResponse = ValidatedUsername.CreateValidatedUsername(username);
        var passwordResponse = ValidatedPassword.CreateValidatedPassword(password);

        var errors = new List<ErrorMessage>();
        var isValidationFailed = false;

        if (emailResponse.DidValidationFail())
        {
            errors.AddRange(emailResponse.ErrorMsg);
            isValidationFailed = true;
        }

        if (usernameResponse.DidValidationFail())
        {
            errors.AddRange(usernameResponse.ErrorMsg);
            isValidationFailed = true;
        }

        if (passwordResponse.DidValidationFail())
        {
            errors.AddRange(passwordResponse.ErrorMsg);
            isValidationFailed = true;
        }

        if (isValidationFailed)
            return new ValidationResponse<ValidatedUserApplicant>(ValidationResponseType.Failed, null, errors);

        return new ValidationResponse<ValidatedUserApplicant>(ValidationResponseType.Success,
            new ValidatedUserApplicant(emailResponse.ValidatedObject!, usernameResponse.ValidatedObject!,
                passwordResponse.ValidatedObject!));
    }
}