using KindPaws.Domain.Managements.SpeciesManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.SpeciesManagement.ValueObjects;

public record BreedColor
{
    private BreedColor(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<BreedColor, IEnumerable<string>> Create(string value)
    {
        List<string> errors = [];

        value.DefaultValidate(
                BreedColorConstraints.MinLength,
                BreedColorConstraints.MaxLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new BreedColor(value);
    }
}