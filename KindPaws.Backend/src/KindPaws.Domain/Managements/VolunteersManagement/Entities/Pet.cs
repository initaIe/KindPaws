using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.Entities;

public class Pet : Entity<PetId>, IFullDeletable
{
    private List<PetPhoto> _photos = [];

    // ef core
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        ShortName name,
        PetType petType)
        : base(id)
    {
        Name = name;
        PetType = petType;
        CreationDateTime = DateTime.UtcNow; // TODO: fix UTC
    }

    public ShortName Name { get; private set; }
    public PetType PetType { get; private set; }
    public DateTime CreationDateTime { get; private set; }
    public SupportStatus? SupportStatus { get; private set; }
    public MediumDescription? Description { get; private set; }
    public PetColor? Color { get; private set; }
    public Age? Age { get; private set; }
    public HealthDetails HealthDetails { get; private set; } = HealthDetails.Empty;
    public BiometricDetails BiometricDetails { get; private set; } = BiometricDetails.Empty;
    public IReadOnlyList<PetPhoto> Photos => _photos;
    public Position Position { get; private set; }
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletedDateTime { get; private set; }
    public bool IsHardDeleted { get; private set; }

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
        Color = petColor;
        Age = age;
        HealthDetails = healthDetails ?? HealthDetails.Empty;
        BiometricDetails = biometricDetails ?? BiometricDetails.Empty;
    }

    public void AddPhotos(IEnumerable<PetPhoto> photos)
    {
        _photos.AddRange(photos.ToList());
    }

    public void DeletePhotos(IEnumerable<PetPhoto> photos)
    {
        foreach (var photo in photos)
            _photos.Remove(photo);
    }

    public void UpdatePosition(Position position)
    {
        Position = position;
    }

    public Result<Error> IncreasePosition()
    {
        var increasedPositionResult = Position.GetIncreased();
        if (increasedPositionResult.IsFailure)
            return increasedPositionResult.Error;

        UpdatePosition(increasedPositionResult.Value);
        return true;
    }

    public Result<Error> DecreasePosition()
    {
        var decreasedPositionResult = Position.GetDecreased();
        if (decreasedPositionResult.IsFailure)
            return decreasedPositionResult.Error;

        UpdatePosition(decreasedPositionResult.Value);
        return true;
    }

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