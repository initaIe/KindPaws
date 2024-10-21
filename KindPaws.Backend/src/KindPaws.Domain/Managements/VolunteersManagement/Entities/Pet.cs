using System.Runtime.InteropServices.JavaScript;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.Entities;

public class Pet : Entity<PetId>, ISoftDeleteable
{
    private readonly List<PetPhoto> _photos;
    private bool _isDeleted;

    // ef core
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        PetType petType,
        ShortName name,
        SupportStatus? supportStatus,
        MediumDescription? description,
        PetColor? petColor,
        Age? age,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails,
        IEnumerable<PetPhoto>? photos)
        : base(id)
    {
        PetType = petType;
        Name = name;
        CreationDateTime = DateTime.UtcNow; // TODO: UTC
        SupportStatus = supportStatus;
        Description = description;
        PetColor = petColor;
        Age = age;
        HealthDetails = healthDetails ?? HealthDetails.CreateNullable();
        BiometricDetails = biometricDetails ?? BiometricDetails.CreateNullable();
        _photos = photos?.ToList() ?? [];
    }

    public PetType PetType { get; private set; }
    public ShortName Name { get; private set; }
    public DateTime CreationDateTime { get; private set; }
    public SupportStatus? SupportStatus { get; private set; }
    public MediumDescription? Description { get; private set; }
    public PetColor? PetColor { get; private set; }
    public Age? Age { get; private set; }
    public HealthDetails HealthDetails { get; private set; }
    public BiometricDetails BiometricDetails { get; private set; }
    public IReadOnlyList<PetPhoto> Photos => _photos;
    public Position Position { get; private set; }

    public void Delete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public void SetPosition(Position position)
    {
        Position = position;
    }
    
    public Result<Error> DecreasePosition()
    {
        var decreasedNumber = Position.Value - Position.ChangeUnit;
        var decreasePosition = Position.Create(decreasedNumber);
        if (decreasePosition.IsFailure)
            return decreasePosition.Error;
        
        Position = decreasePosition.Value;
        return true;
    }
    
    public Result<Error> IncreasePosition()
    {
        var increasedNumber = Position.Value + Position.ChangeUnit;
        var increasePosition = Position.Create(increasedNumber);
        if (increasePosition.IsFailure)
            return increasePosition.Error;
        
        Position = increasePosition.Value;
        return true;
    }

    public void UpdateMainInfo(
        PetType petType,
        ShortName name)
    {
        PetType = petType;
        Name = name;
    }

    public void UpdateAdditionalInfo(
        SupportStatus? supportStatus,
        MediumDescription? description,
        PetColor? petColor,
        Age? age,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails)
    {
        SupportStatus = supportStatus;
        Description = description;
        PetColor = petColor;
        Age = age;
        HealthDetails = healthDetails ?? HealthDetails.CreateNullable();
        BiometricDetails = biometricDetails ?? BiometricDetails.CreateNullable();
    }

    public void AddPhotos(IEnumerable<PetPhoto> photos)
    {
        _photos.AddRange(photos.ToList());
    }
}