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

    public static Result<PetColor, Error> Create(string input)
    {
        input = input.Trim();

        if (!StringValidator.IsInRange(input, PetColorConstraints.MinLength, PetColorConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(input));

        return new PetColor(input);
    }

    public static PetColor CreateEmpty()
    {
        return new PetColor(value: null);
    }
}