using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsRequired(nameof(Vaccine));

        input = input.Trim().ToProperCase();

        if (!StringValidator.IsInRange(input, VaccineConstraints.MinLength, VaccineConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(input));

        return new Vaccine(input);
    }
}