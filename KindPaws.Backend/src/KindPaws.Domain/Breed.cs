using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class Breed
{
    public const int MinNameLength = 1;
    public const int MaxNameLength = 25;
    public const int MinDescriptionLength = 10;
    public const int MaxDescriptionLength = 250;

    private readonly List<BreedColor> _breedColors;

    private Breed(
        Guid id,
        List<BreedColor> breedColors,
        string name,
        string description)
    {
        Id = id;
        _breedColors = breedColors;
        Name = name;
        Description = description;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<BreedColor> BreedColors => _breedColors;

    public static Result<Breed, IEnumerable<string>> Create(
        Guid id,
        List<BreedColor> breedColors,
        string name,
        string description)
    {
        List<string> errors = [];

        id.Validate().AddErrorIfFailure(errors);
        name.DefaultValidate(MinNameLength, MaxNameLength);
        description.DefaultValidate(MinDescriptionLength, MaxDescriptionLength);

        if (errors.Count > 0) return Result.Failure<Breed, IEnumerable<string>>(errors);

        var breed = new Breed(id, breedColors, name, description);

        return Result.Success<Breed, IEnumerable<string>>(breed);
    }
}