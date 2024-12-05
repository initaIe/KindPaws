using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;

public record SpecieName
{
    private SpecieName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<SpecieName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(SpecieName));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsAlphabeticWithWhiteSpaces(input))
            return Errors.General.ValueOutOfRange(nameof(SpecieName));

        if (!StringValidator.IsInRange(input, SpecieNameConstraints.MinLength, SpecieNameConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(SpecieName));

        return new SpecieName(input);
    }
}