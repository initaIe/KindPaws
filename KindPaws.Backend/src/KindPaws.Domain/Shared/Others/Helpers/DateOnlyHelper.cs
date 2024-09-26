namespace KindPaws.Domain.Shared.Others.Helpers;

// TODO: fix code duplication in future
public static class DateOnlyHelper
{
    public static int CalculateYearsPassed(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
            throw new ArgumentException("End date must be greater than or equal to start date.");

        int years = endDate.Year - startDate.Year;

        if (endDate < startDate.AddYears(years))
        {
            years--;
        }

        return years;
    }
    
    public static int CalculateYearsSince(DateOnly startDate)
    {
        DateOnly endDate = DateOnly.FromDateTime(DateTime.Now);

        if (endDate < startDate)
            throw new ArgumentException("Start date must be in the past.");

        int years = endDate.Year - startDate.Year;

        if (endDate < startDate.AddYears(years))
        {
            years--;
        }

        return years;
    }
}