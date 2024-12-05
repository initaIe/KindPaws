using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

public record PetName
{
    private PetName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PetName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(Disease));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsAlphabeticWithWhiteSpaces(input))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(PetName));

        if (!StringValidator.IsInRange(input, PetNameConstraints.MinLength, PetNameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(PetName));

        return new PetName(input);
    }
}