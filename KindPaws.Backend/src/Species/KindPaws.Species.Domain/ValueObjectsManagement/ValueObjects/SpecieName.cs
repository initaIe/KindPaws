using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsRequired(nameof(SpecieName));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsAlphabeticWithWhiteSpaces(input))
            return ErrorsGeneral.ValueOutOfRange(nameof(SpecieName));

        if (!StringValidator.IsInRange(input, SpecieNameConstraints.MinLength, SpecieNameConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(SpecieName));

        return new SpecieName(input);
    }
}