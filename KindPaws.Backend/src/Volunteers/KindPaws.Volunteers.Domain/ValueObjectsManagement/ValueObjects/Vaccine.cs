using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
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
            return GeneralErrors.General.ValueIsRequired(nameof(Vaccine));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsInRange(input, VaccineConstraints.MinLength, VaccineConstraints.MaxLength))
            return GeneralErrors.General.ValueOutOfRange(nameof(input));

        return new Vaccine(input);
    }
}