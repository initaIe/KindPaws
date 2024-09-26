using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Helpers;

namespace KindPaws.Domain.Shared.VOs;

public class Experience
{
    private Experience(DateOnly startDate)
    {
        StartDate = startDate;
    }

    public DateOnly StartDate { get; private set; }
    public int PassedYears => DateOnlyHelper.CalculateYearsSince(StartDate);

    public static Result<Experience, IEnumerable<string>> Create(DateOnly startDate)
    {
        List<string> errors = [];
        
        // TODO: add DateOnly validator
        if (startDate > DateOnly.FromDateTime(DateTime.Now))
            errors.Add("Date can not be earlier than now.");

        if (errors.Count > 0)
            return errors;

        var experience = new Experience(startDate);

        return experience;
    }
}