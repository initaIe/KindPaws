using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

public record BreedDescription
{
    private BreedDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<BreedDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(BreedDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                BreedDescriptionConstraints.MinLength,
                BreedDescriptionConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(BreedDescription));

        return new BreedDescription(input);
    }
}