namespace KindPaws.SharedKernel.Utilities.Helpers;

public static class DateOnlyHelper
{
    public static int CalculateYearsPassed(DateOnly startDate, DateOnly? endDate = null)
    {
        endDate ??= GetDateOnlyNow();

        if (endDate < startDate)
            throw new InvalidOperationException("Start date cannot be early than end date");

        var years = endDate.Value.Year - startDate.Year;

        if (endDate < startDate.AddYears(years)) years--;

        return years;
    }

    public static DateOnly GetDateOnlyNow()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}