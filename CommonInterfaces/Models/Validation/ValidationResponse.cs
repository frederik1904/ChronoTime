namespace Common.Models.Validation;

public class ValidationResponse<T>
{
    public ValidationResponse(ValidationResponseType validationResponseType, T? validatedObject,
        params ErrorMessage[] errors)
    {
        ValidationResponseType = validationResponseType;
        ErrorMsg = new List<ErrorMessage>(errors);
        ValidatedObject = validatedObject;
    }

    public ValidationResponse(ValidationResponseType validationResponseType, T? validatedObject,
        List<ErrorMessage> errors)
    {
        ValidationResponseType = validationResponseType;
        ErrorMsg = errors;
        ValidatedObject = validatedObject;
    }

    public ValidationResponseType ValidationResponseType { get; }
    public List<ErrorMessage> ErrorMsg { get; }

    public T? ValidatedObject { get; }

    public bool DidValidationFail()
    {
        return ValidationResponseType.Failed == ValidationResponseType;
    }
}