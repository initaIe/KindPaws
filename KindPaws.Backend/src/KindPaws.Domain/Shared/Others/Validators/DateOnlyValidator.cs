using KindPaws.Domain.Shared.Others.Helpers;

namespace KindPaws.Domain.Shared.Others.Validators;

public static class DateOnlyValidator
{
    public static bool IsFromFuture(DateOnly date)
    {
        return DateOnlyHelper.GetDateOnlyNow() < date;
    }

    public static bool IsFromPast(DateOnly date)
    {
        return !IsFromFuture(date);
    }
}