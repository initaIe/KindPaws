namespace KindPaws.Domain.Shared.Others.Validators;

public static class FloatValidator
{
    public static Result<string> MinValueValidate(this float input, float minValue)
    {
        if (input < minValue)
            return $"Min value must be larger than or equal {minValue}.";

        return true;
    }
}