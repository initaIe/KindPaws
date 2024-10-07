using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Helpers;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Age
{
    // ef core
    private Age()
    {
    }

    private Age(DateOnly dateBirth)
    {
        DateBirth = dateBirth;
    }

    public DateOnly? DateBirth { get; }

    public int? YearsOld => DateBirth != null
        ? DateOnlyHelper.CalculateYearsPassed(DateBirth.Value)
        : null;

    public static Result<Age, Error> Create(DateOnly input)
    {
        if (DateOnlyValidator.IsFromFuture(input))
            return Errors.General.ValueIsInvalid(nameof(input));

        return new Age(input);
    }
}