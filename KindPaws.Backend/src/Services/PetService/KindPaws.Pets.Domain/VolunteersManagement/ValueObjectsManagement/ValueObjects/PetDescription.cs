using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

public record PetDescription
{
    private PetDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PetDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.ValueIsRequired(nameof(PetDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(input, PetDescriptionConstraints.MinLength, PetDescriptionConstraints.MaxLength))
            return GeneralErrors.ValueOutOfRange(nameof(PetDescription));

        return new PetDescription(input);
    }
}