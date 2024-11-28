using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Helpers;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Domain.Entities;

public class Pet : ISoftDeletableEntity<PetId>
{
    private List<PetPhoto> _photos = [];

    // ef core
    private Pet()
    {
    }

    public Pet(
        PetId id,
        PetName name,
        PetType petType,
        CreationTimestamp creationTimestamp)
    {
        Id = id;
        Name = name;
        PetType = petType;
        CreationTimestamp = creationTimestamp;
    }

    public PetId Id { get; }
    public PetName Name { get; private set; }
    public PetType PetType { get; private set; }
    public CreationTimestamp CreationTimestamp { get; private set; }
    public SupportStatus? SupportStatus { get; private set; }
    public PetDescription? Description { get; private set; }
    public PetColor? Color { get; private set; }
    public Birthday? Birthday { get; private set; }
    public HealthDetails? HealthDetails { get; private set; }
    public BiometricDetails? BiometricDetails { get; private set; }
    public IReadOnlyList<PetPhoto> Photos => _photos;
    public Position Position { get; private set; }
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletionTimestamp { get; private set; }

    #region Factory methods

    public static Pet CreateNew(
        PetName name,
        PetType petType)
    {
        var id = PetId.CreateRandom();
        var creationTimestamp = CreationTimestamp.CreateNew();

        return new Pet(
            id,
            name,
            petType,
            creationTimestamp);
    }

    #endregion

    #region CRUD

    public int? YearsOld => Birthday == null ? null : DateTimeHelpers.CalculateYearsPassed(Birthday.Value);

    internal void UpdateMainInfo(
        PetType petType,
        PetName name)
    {
        PetType = petType;
        Name = name;
    }

    internal void UpdateAdditionalInfo(
        SupportStatus? supportStatus,
        PetDescription? description,
        PetColor? petColor,
        Birthday? age,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails)
    {
        SupportStatus = supportStatus;
        Description = description;
        Color = petColor;
        Birthday = age;
        HealthDetails = healthDetails ?? HealthDetails.Empty;
        BiometricDetails = biometricDetails ?? BiometricDetails.Empty;
    }

    internal void AddPhotos(IEnumerable<PetPhoto> photos)
    {
        _photos.AddRange(photos.ToList());
    }

    internal Result<Error> SetMainPhoto(FilePath photoFilePath)
    {
        var petPhoto = _photos.FirstOrDefault(p => p.Photo.FilePath == photoFilePath);
        if (petPhoto == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetPhoto));

        var oldMainPhoto = _photos.FirstOrDefault(p => p.IsMain);
        if (oldMainPhoto != null)
        {
            _photos.Remove(oldMainPhoto);
            var updatedOldMainPetPhoto = new PetPhoto(oldMainPhoto.Photo, false);
            _photos.Add(updatedOldMainPetPhoto);
        }

        _photos.Remove(petPhoto);
        var newMainPhoto = new PetPhoto(petPhoto.Photo, true);
        _photos.Add(newMainPhoto);

        return true;
    }

    internal void DeletePhotos(IEnumerable<PetPhoto> photos)
    {
        foreach (var photo in photos)
            _photos.Remove(photo);
    }

    internal void UpdatePosition(Position position)
    {
        Position = position;
    }

    internal Result<Error> IncreasePosition()
    {
        var increasedPositionResult = Position.GetIncreased();
        if (increasedPositionResult.IsFailure)
            return increasedPositionResult.Error;

        UpdatePosition(increasedPositionResult.Value);
        return true;
    }

    internal Result<Error> DecreasePosition()
    {
        var decreasedPositionResult = Position.GetDecreased();
        if (decreasedPositionResult.IsFailure)
            return decreasedPositionResult.Error;

        UpdatePosition(decreasedPositionResult.Value);
        return true;
    }

    internal void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletionTimestamp = DateTime.UtcNow;
    }

    internal void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletionTimestamp = null;
        // TODO: give position after restore
    }

    #endregion
}