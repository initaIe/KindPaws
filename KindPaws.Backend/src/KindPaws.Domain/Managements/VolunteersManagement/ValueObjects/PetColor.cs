using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record PetColor
{
    private PetColor(string? value)
    {
        Value = value;
    }

    public string? Value { get; }

    public static Result<PetColor, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid(nameof(value));

        if (!StringValidator.IsInRange(value, PetColorConstraints.MinLength, PetColorConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(value));

        return new PetColor(value);
    }

    public static PetColor CreateEmpty()
    {
        return new PetColor(value: null);
    }
}