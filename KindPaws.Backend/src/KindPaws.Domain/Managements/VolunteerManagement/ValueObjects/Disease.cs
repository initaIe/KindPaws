using KindPaws.Domain.Managements.VolunteerManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteerManagement.ValueObjects;

public record Disease
{
    public Disease()
    {
    }

    private Disease(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Disease, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];

        value.DefaultValidate(
                DiseaseConstraints.MinLength,
                DiseaseConstraints.MaxLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Disease(value);
    }
}