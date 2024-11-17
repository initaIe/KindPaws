namespace KindPaws.SharedKernel.Utilities.Helpers;

public static class DateTimeHelpers
{
    public static int CalculateYearsPassed(DateTime startDate, DateTime? endDate = null)
    {
        endDate ??= DateTime.UtcNow;

        if (endDate < startDate)
            throw new InvalidOperationException("Start date cannot be early than end date");

        var years = endDate.Value.Year - startDate.Year;

        if (endDate < startDate.AddYears(years)) years--;

        return years;
    }
}