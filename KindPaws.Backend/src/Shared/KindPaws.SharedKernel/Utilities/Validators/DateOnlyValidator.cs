using KindPaws.SharedKernel.Utilities.Helpers;

namespace KindPaws.SharedKernel.Utilities.Validators;

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