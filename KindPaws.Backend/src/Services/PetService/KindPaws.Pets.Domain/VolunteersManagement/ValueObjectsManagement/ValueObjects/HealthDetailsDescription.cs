using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

public record HealthDetailsDescription
{
    private HealthDetailsDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<HealthDetailsDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.ValueIsRequired(nameof(HealthDetailsDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                HealthDetailsDescriptionConstraints.MinLength,
                HealthDetailsDescriptionConstraints.MaxLength))
            return GeneralErrors.ValueOutOfRange(nameof(HealthDetailsDescription));

        return new HealthDetailsDescription(input);
    }
}