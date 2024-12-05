using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;

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
            return Errors.General.ValueIsRequired(nameof(SpecieDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                SpecieDescriptionConstraints.MinLength,
                SpecieDescriptionConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(SpecieDescription));

        return new SpecieDescription(input);
    }
}