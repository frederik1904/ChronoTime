using System.Net.Mail;
using static Common.Models.Validation.ErrorMessages.ValidationFields;
using static Common.Models.Validation.ErrorMessages.ValidationTexts;
using static Common.Models.Validation.ValidationResponseType;

namespace Common.Models.Validation;

public class ValidatedEmail
{
    private ValidatedEmail(string email)
    {
        Email = email;
    }

    public string Email { get; }

    public static ValidationResponse<ValidatedEmail> CreateValidatedEmail(string email)
    {
        var trimmedEmail = email.Trim().ToLower();
        var isValidEmail = IsValidEmail(trimmedEmail);
        return isValidEmail
            ? new ValidationResponse<ValidatedEmail>(Success,
                new ValidatedEmail(trimmedEmail))
            : new ValidationResponse<ValidatedEmail>(Failed, null, new ErrorMessage(FAILED_VALIDATING_EMAIL, EMAIL));
    }

    private static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) return false;

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}