using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Helpers;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Age
{
    private Age(DateOnly? dateBirth)
    {
        DateBirth = dateBirth;
    }

    public DateOnly? DateBirth { get; }

    public int? YearsOld =>
        DateBirth != null
            ? DateOnlyHelper.CalculateYearsPassed(DateBirth.Value)
            : null;

    public static Result<Age, Error> Create(DateOnly dateBirth)
    {
        if (DateOnlyValidator.IsFromFuture(dateBirth))
            return Errors.General.ValueIsInvalid(nameof(dateBirth));

        return new Age(dateBirth);
    }

    public static Age CreateEmpty()
    {
        return new Age(dateBirth: null);
    }
}