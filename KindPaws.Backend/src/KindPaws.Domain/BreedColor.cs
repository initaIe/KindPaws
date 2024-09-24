using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class BreedColor
{
    private BreedColor(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Result<BreedColor, IEnumerable<string>> Create(string name)
    {
        List<string> errors = [];

        name.DefaultValidate(
                BreedColorRules.MinNameLength,
                BreedColorRules.MaxNameLength)
            .AddErrorsIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<BreedColor, IEnumerable<string>>(errors);

        var breed = new BreedColor(name);

        return Result.Success<BreedColor, IEnumerable<string>>(breed);
    }
}