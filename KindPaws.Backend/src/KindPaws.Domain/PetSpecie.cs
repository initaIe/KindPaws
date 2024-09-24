using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class PetSpecie
{
    private readonly List<Breed> _breeds;

    private PetSpecie(
        List<Breed> breeds,
        string name,
        string description)
    {
        _breeds = breeds;
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<PetSpecie, IEnumerable<string>> Create(
        List<Breed> breeds,
        string name,
        string description)
    {
        List<string> errors = [];

        name.DefaultValidate(
                PetSpecieRules.MinNameLength,
                PetSpecieRules.MaxNameLength)
            .AddErrorIfFailure(errors);

        description.DefaultValidate(
                PetSpecieRules.MinDescriptionLength,
                PetSpecieRules.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0) return Result.Failure<PetSpecie, IEnumerable<string>>(errors);

        var petSpecie = new PetSpecie(breeds, name, description);

        return Result.Success<PetSpecie, IEnumerable<string>>(petSpecie);
    }
}