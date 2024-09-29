using KindPaws.Domain.Managements.BreedManagement.Constraints;
using KindPaws.Domain.Managements.BreedManagement.ValuseObjects.Lists;
using KindPaws.Domain.Managements.PetManagement.AggregateRoot;
using KindPaws.Domain.Managements.SpecieManagement.AggregateRoot;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.BreedManagement.AggregateRoot;

public class Breed : Entity<BreedId>
{
    private readonly List<Pet> _pets;

    public Breed(BreedId id) : base(id)
    {
    }

    public Breed(
        BreedId id,
        List<Pet> pets,
        string name,
        string description,
        SpecieId specieId,
        Specie specie,
        BreedColorList colorList)
        : base(id)
    {
        _pets = pets;
        Name = name;
        Description = description;
        SpecieId = specieId;
        Specie = specie;
        ColorList = colorList;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public SpecieId SpecieId { get; private set; }
    public Specie Specie { get; private set; }
    public BreedColorList ColorList { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

    public static Result<Breed, IEnumerable<string>> Create(
        BreedId id,
        List<Pet> pets,
        string name,
        string description,
        SpecieId specieId,
        Specie specie,
        BreedColorList colorList)
    {
        List<string> errors = [];

        name.DefaultValidate(
                BreedConstraints.MinNameLength,
                BreedConstraints.MaxNameLength)
            .AddErrorIfFailure(errors);

        description.DefaultValidate(
                BreedConstraints.MinDescriptionLength,
                BreedConstraints.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Breed(
            id,
            pets,
            name,
            description,
            specieId,
            specie,
            colorList);
    }
}