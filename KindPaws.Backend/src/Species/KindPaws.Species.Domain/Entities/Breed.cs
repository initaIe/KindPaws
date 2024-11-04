using KindPaws.SharedKernel.Others.DeletionManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Species.Domain.Entities;

public class Breed : Entity<BreedId>, IFullDeletable
{
    // ef core
    private Breed(BreedId id) : base(id)
    {
    }

    public Breed(
        BreedId id,
        ShortName name,
        MediumDescription description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public ShortName Name { get; private set; } // TODO: create new VO for breed name with alphabetic with white spaces
    public MediumDescription Description { get; private set; }
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletedDateTime { get; private set; }
    public bool IsHardDeleted { get; private set; }

    public void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletedDateTime = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletedDateTime = null;
    }

    public void HardDelete()
    {
        IsHardDeleted = true;
    }
}