namespace KindPaws.Domain.Helpers;

public static class FloatHelper
{
    public static float Round(this float number, int decimalPartLength, bool roundUp)
    {
        if (decimalPartLength < 0)
            throw new Exception("The decimal part length cannot be negative.");

        var factor = (float)Math.Pow(10, decimalPartLength);

        if (roundUp) return (float)Math.Ceiling(number * factor) / factor;

        return (float)Math.Floor(number * factor) / factor;
    }
}