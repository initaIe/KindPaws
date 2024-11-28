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
        CreationTimestamp creationTimestamp)
    {
        Id = id;
        Name = name;
        Description = description;
        CreationTimestamp = creationTimestamp;
    }

    public BreedId Id { get; }
    public BreedName Name { get; private set; }
    public BreedDescription Description { get; private set; }
    public CreationTimestamp CreationTimestamp { get; private set; }
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletionTimestamp { get; private set; }

    #region Factory methods

    public static Breed CreateNew(
        BreedName name,
        BreedDescription description)
    {
        var id = BreedId.CreateRandom();
        var creationTimestamp = CreationTimestamp.CreateNew();

        return new Breed(
            id,
            name,
            description,
            creationTimestamp);
    }

    #endregion

    #region CRUD

    internal void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletionTimestamp = DateTime.UtcNow;
    }

    internal void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletionTimestamp = null;
    }

    #endregion
}