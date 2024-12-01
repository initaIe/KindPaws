using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Species.Domain.Entities;

public class Breed : ISoftDeletableEntity<BreedId>
{
    // ef core
    private Breed()
    {
    }

    public Breed(
        BreedId id,
        BreedName name,
        BreedDescription description,
        CreatedAt createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    public BreedId Id { get; private set; }
    public BreedName Name { get; private set; }
    public BreedDescription Description { get; private set; }
    public CreatedAt CreatedAt { get; private set; }
    public bool IsSoftDeleted { get; private set; }
    public DateTimeOffset? SoftDeletedAt { get; private set; }

    #region Factory methods

    public static Breed CreateNew(
        BreedName name,
        BreedDescription description)
    {
        var id = BreedId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Breed(
            id,
            name,
            description,
            createdAt);
    }

    #endregion

    #region CRUD

    internal void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletedAt = DateTimeOffset.UtcNow;
    }

    internal void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletedAt = null;
    }

    #endregion
}