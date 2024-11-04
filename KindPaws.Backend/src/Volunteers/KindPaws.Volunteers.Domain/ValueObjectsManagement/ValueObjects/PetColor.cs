using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record PetColor
{
    private PetColor(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PetColor, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(PetColor));

        input = input.Trim();

        if (!StringValidator.IsInRange(input, PetColorConstraints.MinLength, PetColorConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(input));

        return new PetColor(input);
    }
}