using CommonModels.Wrappers;
using static Common.Models.Validation.ValidationResponseType;

namespace Common.Models.Validation;

public class ValidatedPassword
{
    public readonly Secret<string> Password;

    private ValidatedPassword(string password)
    {
        Password = new Secret<string>(password);
    }

    public static ValidationResponse<ValidatedPassword> CreateValidatedPassword(string password)
    {
        return new ValidationResponse<ValidatedPassword>(Success,
            new ValidatedPassword(password));
    }
}