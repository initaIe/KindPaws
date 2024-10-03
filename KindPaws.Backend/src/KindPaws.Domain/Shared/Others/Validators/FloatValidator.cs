namespace KindPaws.Domain.Shared.Others.Validators;

public static class FloatValidator
{
    public static bool IsNotLessThan(float value, float minValue)
    {
        return value < minValue;
    }
}