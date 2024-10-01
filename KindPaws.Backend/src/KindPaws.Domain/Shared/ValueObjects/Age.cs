using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Helpers;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Age
{
    private Age()
    {
    }

    private Age(DateOnly birthDate)
    {
        DateBirth = birthDate;
    }

    public DateOnly DateBirth { get; }
    public int YearsOld => DateOnlyHelper.CalculateYearsSince(DateBirth);

    public static Result<Age, IEnumerable<string>> Create(DateOnly birthDate)
    {
        List<string> errors = [];

        birthDate.PastDateOnlyValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Age(birthDate);
    }
}