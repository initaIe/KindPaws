using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Vaccine
{
    private Vaccine(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Vaccine, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid(nameof(value));

        if (!StringValidator.IsInRange(value, VaccineConstraints.MinLength, VaccineConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(value));

        return new Vaccine(value);
    }
}