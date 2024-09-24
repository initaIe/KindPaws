using CSharpFunctionalExtensions;

namespace KindPaws.Domain.Validators;

public static class FloatValidator
{
    public static Result<float, string> MinValueValidate(this float input, float minValue)
    {
        if (input < minValue)
        {
            var error = $"Min value must be larger than or equal {minValue}.";
            return Result.Failure<float, string>(error);
        }

        return Result.Success<float, string>(input);
    }
}