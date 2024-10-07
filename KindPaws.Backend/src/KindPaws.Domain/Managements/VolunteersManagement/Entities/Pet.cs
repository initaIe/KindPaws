using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.Entities;

public class Pet : Entity<PetId>, ISoftDeleteable
{
    private bool _isDeleted;
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        PetType petType,
        ShortName name,
        DateTime creationDateTime,
        SupportStatus supportStatus)
        : base(id)
    {
        PetType = petType;
        Name = name;
        CreationDateTime = creationDateTime;
        SupportStatus = supportStatus;
    }

    public PetType PetType { get; private set; }
    public ShortName Name { get; private set; }
    public DateTime CreationDateTime { get; private set; }
    public SupportStatus SupportStatus { get; private set; }
    public MediumDescription? Description { get; }
    public PetColor? PetColor { get; }
    public Age? Age { get; }
    public HealthDetails HealthDetails { get; }
    public BiometricDetails BiometricDetails { get; }
    public PetPhotoList PetPhotoList { get; private set; } = new([]);
    
    public void Delete()
    {
        _isDeleted = true;
    }
    
    public void Restore()
    {
        _isDeleted = false;
    }
}