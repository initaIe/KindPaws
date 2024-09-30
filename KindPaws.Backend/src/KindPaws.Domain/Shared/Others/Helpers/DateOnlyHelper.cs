namespace KindPaws.Domain.Shared.Others.Helpers;

// TODO: fix code duplication in future
public static class DateOnlyHelper
{
    public static int CalculateYearsPassed(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
            throw new InvalidOperationException("End date must be greater than or equal to start date.");

        var years = endDate.Year - startDate.Year;

        if (endDate < startDate.AddYears(years)) years--;

        return years;
    }

    public static int CalculateYearsSince(DateOnly startDate)
    {
        var nowDate = GetDateOnlyNow();

        if (nowDate < startDate)
            throw new ArgumentException("End date must be greater than or equal to start date.");

        var years = nowDate.Year - startDate.Year;

        if (nowDate < startDate.AddYears(years)) years--;

        return years;
    }

    public static DateOnly GetDateOnlyNow()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}