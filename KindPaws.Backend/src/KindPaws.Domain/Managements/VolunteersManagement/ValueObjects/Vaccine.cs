using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Vaccine
{
    private Vaccine(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Vaccine, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(Vaccine));

        input = input.Trim();

        if (!StringValidator.IsInRange(input, VaccineConstraints.MinLength, VaccineConstraints.MaxLength))
            return Errors.General.ValueOutOfRange(nameof(input));

        return new Vaccine(input);
    }
}