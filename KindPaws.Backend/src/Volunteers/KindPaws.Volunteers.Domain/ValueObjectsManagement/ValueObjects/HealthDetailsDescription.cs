using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsRequired(nameof(HealthDetailsDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                HealthDetailsDescriptionConstraints.MinLength,
                HealthDetailsDescriptionConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(HealthDetailsDescription));

        return new HealthDetailsDescription(input);
    }
}