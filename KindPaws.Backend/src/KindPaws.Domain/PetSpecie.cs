using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class PetSpecie
{
    public const int MinNameLength = 1;
    public const int MaxNameLength = 25;
    public const int MinDescriptionLength = 10;
    public const int MaxDescriptionLength = 250;

    private readonly List<Breed> _breeds;

    private PetSpecie(Guid id, List<Breed> breeds, string name, string description)
    {
        Id = id;
        _breeds = breeds;
        Name = name;
        Description = description;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<PetSpecie, IEnumerable<string>> Create(
        Guid id,
        List<Breed> breeds,
        string name,
        string description)
    {
        List<string> errors = [];

        id.Validate().AddErrorIfFailure(errors);
        name.DefaultValidate(MinNameLength, MaxNameLength).AddErrorsIfFailure(errors);
        description.DefaultValidate(MinDescriptionLength, MaxDescriptionLength).AddErrorsIfFailure(errors);

        if (errors.Count > 0) return Result.Failure<PetSpecie, IEnumerable<string>>(errors);

        var petSpecie = new PetSpecie(id, breeds, name, description);

        return Result.Success<PetSpecie, IEnumerable<string>>(petSpecie);
    }
}