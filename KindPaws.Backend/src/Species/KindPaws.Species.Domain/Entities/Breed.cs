using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
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
        BreedDescription description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public BreedId Id { get; }
    public BreedName Name { get; private set; }
    public BreedDescription Description { get; private set; }
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletionTimestamp { get; private set; }

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
}