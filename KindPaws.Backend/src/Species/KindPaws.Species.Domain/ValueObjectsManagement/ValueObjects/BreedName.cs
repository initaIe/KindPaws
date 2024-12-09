using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

public record BreedName
{
    private BreedName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<BreedName, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(BreedName));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsAlphabeticWithWhiteSpaces(input))
            return ErrorsGeneral.ValueOutOfRange(nameof(BreedName));

        if (!StringValidator.IsInRange(input, BreedNameConstraints.MinLength, BreedNameConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(BreedName));

        return new BreedName(input);
    }
}