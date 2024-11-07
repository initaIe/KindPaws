using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Helpers;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

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