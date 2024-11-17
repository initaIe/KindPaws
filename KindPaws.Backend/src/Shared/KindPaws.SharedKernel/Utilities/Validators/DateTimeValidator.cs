using KindPaws.SharedKernel.Utilities.Helpers;

namespace KindPaws.SharedKernel.Utilities.Validators;

public static class DateTimeValidator
{
    public static bool IsFromFuture(DateTime date)
    {
        return DateTime.UtcNow < date;
    }

    public static bool IsFromPast(DateTime date)
    {
        return !IsFromFuture(date);
    }
}