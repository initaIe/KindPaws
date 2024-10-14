using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Helpers;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Age
{
    private Age(DateOnly dateBirth)
    {
        DateBirth = dateBirth;
    }

    public DateOnly DateBirth { get; }

    public int YearsOld => DateOnlyHelper.CalculateYearsPassed(DateBirth);

    public static Result<Age, Error> Create(DateOnly input)
    {
        if (DateOnlyValidator.IsFromFuture(input))
            return Errors.General.ValueOutOfRange(nameof(Age));

        return new Age(input);
    }
}