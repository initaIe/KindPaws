using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Pets.Domain.SpeciesManagement.Entities;

public class Breed : Entity<BreedId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Breed(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        BreedId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

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