using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Domain.Entities;

public class Breed : Entity<BreedId>, ISoftDeletable
{
    // ef core
    private Breed(BreedId id) : base(id)
    {
    }

    public Breed(
        BreedId id,
        ShortAlphabeticWhiteSpacesString name,
        MediumString description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public ShortAlphabeticWhiteSpacesString Name { get; private set; }
    public MediumString Description { get; private set; }
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