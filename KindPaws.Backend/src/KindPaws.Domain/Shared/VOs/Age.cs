using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Helpers;

namespace KindPaws.Domain.Shared.VOs;

public record Age
{
    private Age(DateOnly birthDate)
    {
        BirthDate = birthDate;
    }

    public DateOnly BirthDate { get; private set; }
    public int YearsOld => DateOnlyHelper.CalculateYearsSince(BirthDate);

    public static Result<Age, IEnumerable<string>> Create(DateOnly birthDate)
    {
        List<string> errors = [];

        // TODO: add DateOnly validator
        if (birthDate > DateOnly.FromDateTime(DateTime.Now))
            errors.Add("Date can not be earlier than now.");

        if (errors.Count > 0)
            return errors;

        var ageInfo = new Age(birthDate);

        return ageInfo;
    }
}