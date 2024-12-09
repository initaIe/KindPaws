using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

public record SpecieDescription
{
    private SpecieDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<SpecieDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.ValueIsRequired(nameof(SpecieDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                SpecieDescriptionConstraints.MinLength,
                SpecieDescriptionConstraints.MaxLength))
            return GeneralErrors.ValueOutOfRange(nameof(SpecieDescription));

        return new SpecieDescription(input);
    }
}