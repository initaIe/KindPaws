using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsRequired(nameof(PetDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(input, PetDescriptionConstraints.MinLength, PetDescriptionConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(PetDescription));

        return new PetDescription(input);
    }
}