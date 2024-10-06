namespace KindPaws.Domain.Shared.Others.Validation.Validators;

public static class FloatValidator
{
    public static bool IsNotLessThan(float value, float minValue)
    {
        return value < minValue;
    }
}