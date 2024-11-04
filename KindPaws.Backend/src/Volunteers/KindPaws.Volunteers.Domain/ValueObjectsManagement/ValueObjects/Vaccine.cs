using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

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