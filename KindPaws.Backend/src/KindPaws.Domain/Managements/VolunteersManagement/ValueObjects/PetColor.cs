using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

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