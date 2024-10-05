using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Disease
{
    private Disease(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Disease, Error> Create(string input)
    {
        input = input.Trim();

        if (!StringValidator.IsInRange(input, DiseaseConstraints.MinLength, DiseaseConstraints.MaxLength))
            return Errors.General.ValueWrongLength(nameof(input));

        return new Disease(input);
    }
}