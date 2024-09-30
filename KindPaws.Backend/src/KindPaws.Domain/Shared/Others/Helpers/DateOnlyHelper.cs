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

    public static int CalculateYearsSince(DateOnly? currentDate)
    {
        if (currentDate is null)
            throw new ArgumentException("Current date can not be null.");
        
        var nowDate = GetDateOnlyNow();

        if (nowDate < currentDate)
            throw new ArgumentException("Current date can not be from the future.");

        var years = nowDate.Year - currentDate.Value.Year;

        if (nowDate < currentDate.Value.AddYears(years)) years--;

        return years;
    }

    public static DateOnly GetDateOnlyNow()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}