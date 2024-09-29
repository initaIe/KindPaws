using KindPaws.Domain.Managements.BreedManagement.AggregateRoot;
using KindPaws.Domain.Managements.PetManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpecieManagement.Constraints;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.SpecieManagement.AggregateRoot;

public class Specie : Entity<SpecieId>
{
    private readonly List<Breed> _breeds;
    private readonly List<Pet> _pets;

    public Specie(SpecieId id) : base(id)
    {
    }

    public Specie(
        SpecieId id,
        List<Breed> breeds,
        List<Pet> pets,
        string name,
        string description)
        : base(id)
    {
        _breeds = breeds;
        _pets = pets;
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<Breed> Breeds => _breeds;

    public static Result<Specie, IEnumerable<string>> Create(
        SpecieId id,
        List<Breed> breeds,
        List<Pet> pets,
        string name,
        string description)
    {
        List<string> errors = [];

        name.DefaultValidate(
                SpecieConstraints.MinNameLength,
                SpecieConstraints.MaxNameLength)
            .AddErrorIfFailure(errors);

        description.DefaultValidate(
                SpecieConstraints.MinDescriptionLength,
                SpecieConstraints.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Specie(
            id,
            breeds,
            pets,
            name,
            description);
    }
}