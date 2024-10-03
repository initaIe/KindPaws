using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Disease
{
    private Disease(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Disease, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid(nameof(value));

        if (!StringValidator.IsInRange(value, DiseaseConstraints.MinLength, DiseaseConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(value));

        return new Disease(value);
    }
}