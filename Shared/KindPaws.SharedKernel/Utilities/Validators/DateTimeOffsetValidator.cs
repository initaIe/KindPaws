namespace KindPaws.SharedKernel.Utilities.Validators;

public static class DateTimeOffsetValidator
{
    public static bool IsFromFuture(DateTimeOffset date)
    {
        return DateTimeOffset.UtcNow < date;
    }

    public static bool IsFromPast(DateTimeOffset date)
    {
        return !IsFromFuture(date);
    }
}