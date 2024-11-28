namespace KindPaws.SharedKernel.Utilities.Helpers;

public static class DateTimeOffsetHelpers
{
    public static int CalculateYearsPassed(DateTimeOffset startDate, DateTimeOffset? endDate = null)
    {
        endDate ??= DateTimeOffset.UtcNow;

        if (endDate < startDate)
            throw new InvalidOperationException("Start date cannot be early than end date");

        var years = endDate.Value.Year - startDate.Year;

        if (endDate < startDate.AddYears(years)) years--;

        return years;
    }
}