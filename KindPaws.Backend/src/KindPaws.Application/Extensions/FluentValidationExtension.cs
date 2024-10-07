using FluentValidation.Results;

namespace KindPaws.Application.Extensions;

public static class FluentValidationExtension
{
    public static bool IsInvalid(this ValidationResult validationResult)
    {
        return !validationResult.IsValid;
    }
}