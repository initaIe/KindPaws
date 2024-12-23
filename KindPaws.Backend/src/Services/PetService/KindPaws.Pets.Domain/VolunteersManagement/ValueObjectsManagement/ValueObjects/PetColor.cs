using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsRequired(nameof(PetColor));

        input = input.Trim();

        if (!StringValidator.IsInRange(input, PetColorConstraints.MinLength, PetColorConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(input));

        return new PetColor(input);
    }
}