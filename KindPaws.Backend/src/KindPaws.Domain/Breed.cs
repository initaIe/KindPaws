using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class Breed
{
    private readonly List<BreedColor> _breedColors;

    private Breed(
        string name,
        string description,
        List<BreedColor> breedColors)
    {
        Name = name;
        Description = description;
        _breedColors = breedColors;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<BreedColor> BreedColors => _breedColors;

    public static Result<Breed, IEnumerable<string>> Create(
        List<BreedColor> breedColors,
        string name,
        string description)
    {
        List<string> errors = [];

        name.DefaultValidate(
                BreedRules.MinNameLength,
                BreedRules.MaxNameLength)
            .AddErrorsIfFailure(errors);
        description.DefaultValidate(
                BreedRules.MinDescriptionLength,
                BreedRules.MaxDescriptionLength)
            .AddErrorsIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<Breed, IEnumerable<string>>(errors);

        var breed = new Breed(name, description, breedColors);

        return Result.Success<Breed, IEnumerable<string>>(breed);
    }
}