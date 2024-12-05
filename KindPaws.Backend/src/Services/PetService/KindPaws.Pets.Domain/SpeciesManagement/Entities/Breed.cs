using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Pets.Domain.SpeciesManagement.Entities;

public class Breed : Entity<BreedId>
{
    // ef core
    private Breed(
        BreedId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    private Breed(
        BreedId id,
        CreatedAt createdAt,
        BreedName name,
        BreedDescription description)
        : base(id, createdAt)
    {
        Name = name;
        Description = description;
    }

    public BreedName Name { get; private set; }
    public BreedDescription Description { get; private set; }

    #region Factory methods

    public static Breed CreateNew(
        BreedName name,
        BreedDescription description)
    {
        var id = BreedId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Breed(
            id,
            createdAt,
            name,
            description);
    }

    public static Breed Create(
        BreedId id,
        CreatedAt createdAt,
        BreedName name,
        BreedDescription description)
    {
        return new Breed(
            id,
            createdAt,
            name,
            description);
    }

    #endregion

    #region CRUD

    internal void UpdateInfo(
        BreedName name,
        BreedDescription description)
    {
        Name = name;
        Description = description;
        UpdateLastModifiedAt();
    }

    #endregion
}