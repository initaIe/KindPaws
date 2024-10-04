using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.Entities;

public class Pet : Entity<PetId>
{
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        ShortName name,
        MediumDescription? description,
        PetType petType,
        PetColor? petColor,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails,
        Age? age,
        SupportStatus? supportStatus,
        PetPhotoList? petPhotoList)
        : base(id)
    {
        Name = name;
        Description = description ?? MediumDescription.CreateEmpty();
        PetType = petType;
        PetColor = petColor ?? PetColor.CreateEmpty();
        HealthDetails = healthDetails ?? HealthDetails.CreateEmpty();
        BiometricDetails = biometricDetails ?? BiometricDetails.CreateEmpty();
        Age = age ?? Age.CreateEmpty();
        SupportStatus = supportStatus ?? SupportStatus.CreateEmpty();
        PetPhotoList = petPhotoList ?? new PetPhotoList([]);
        CreationDateTime = DateTime.Now;
    }

    public ShortName Name { get; private set; }
    public MediumDescription Description { get; private set; }
    public PetType PetType { get; private set; }
    public PetColor PetColor { get; private set; }
    public HealthDetails HealthDetails { get; private set; }
    public BiometricDetails BiometricDetails { get; private set; }
    public Age Age { get; private set; }
    public SupportStatus SupportStatus { get; private set; }
    public PetPhotoList PetPhotoList { get; private set; }
    public DateTime CreationDateTime { get; private set; }
}