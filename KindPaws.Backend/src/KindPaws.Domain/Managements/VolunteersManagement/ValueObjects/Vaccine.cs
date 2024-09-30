using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Vaccine
{
    private Vaccine(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Vaccine, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];

        value.DefaultValidate(
                VaccineConstraints.MinLength,
                VaccineConstraints.MaxLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Vaccine(value);
    }
}