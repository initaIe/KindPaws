using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Helpers;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Pets.Domain.VolunteersManagement.Entities;

// TODO: AVATAR + PHOTOS
public class Pet : Entity<PetId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Pet(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        PetId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private Pet(
        PetId id,
        CreatedAt createdAt,
        PetName name,
        PetType type)
        : base(id, createdAt)
    {
        Name = name;
        Type = type;
    }

    public PetName Name { get; private set; }
    public PetType Type { get; private set; }
    public SupportStatus SupportStatus { get; private set; } = SupportStatus.Undefined;
    public PetDescription? Description { get; private set; }
    public BirthdayAt? BirthdayAt { get; private set; }
    public HealthDetails? HealthDetails { get; private set; }
    public BiometricDetails? BiometricDetails { get; private set; }

    #region Properties

    public int? YearsOld => BirthdayAt == null ? null : DateTimeOffsetHelpers.CalculateYearsPassed(BirthdayAt.Value);

    #endregion

    #region Factory methods

    public static Pet CreateNew(
        PetName name,
        PetType type)
    {
        var id = PetId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Pet(
            id,
            createdAt,
            name,
            type);
    }

    public static Pet Create(
        PetId id,
        CreatedAt createdAt,
        PetName name,
        PetType type)
    {
        return new Pet(
            id,
            createdAt,
            name,
            type);
    }

    #endregion

    #region CRUD

    internal void UpdateMainInfo(
        PetName name,
        PetType type)
    {
        Name = name;
        Type = type;
        UpdateLastModifiedAt();
    }

    internal void UpdateOtherInfo(
        PetDescription? description,
        BirthdayAt birthdayAt,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails)
    {
        Description = description;
        BirthdayAt = birthdayAt;
        HealthDetails = healthDetails;
        BiometricDetails = biometricDetails;
        UpdateLastModifiedAt();
    }

    #endregion
}