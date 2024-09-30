using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Helpers;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.Entities;

public class Pet : Entity<PetId>
{
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        string name,
        string description,
        PetType petType,
        string color,
        HealthDetails healthDetails,
        BiometricDetails biometricDetails,
        Address address,
        Age age,
        SupportDetails supportDetails,
        PetPhotoList photosList)
        : base(id)
    {
        Name = name;
        Description = description;
        PetType = petType;
        Color = color;
        HealthDetails = healthDetails;
        BiometricDetails = biometricDetails;
        Address = address;
        Age = age;
        SupportDetails = supportDetails;
        PhotosList = photosList;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public PetType PetType { get; private set; }
    public string Color { get; private set; }
    public HealthDetails HealthDetails { get; private set; }
    public BiometricDetails BiometricDetails { get; private set; }
    // TODO: replace to volunteer???
    public Address Address { get; private set; }
    public Age Age { get; private set; }
    public SupportDetails SupportDetails { get; private set; }
    public PetPhotoList PhotosList { get; private set; }
    public DateOnly CreationDate { get; private set; } = DateOnlyHelper.GetDateOnlyNow();

    public static Result<Pet, IEnumerable<string>> Create(
        PetId id,
        string name,
        string description,
        PetType petType,
        string color,
        HealthDetails healthDetails,
        BiometricDetails characteristicsDetails,
        Address address,
        Age age,
        SupportDetails supportDetails,
        PetPhotoList photosList)
    {
        List<string> errors = [];

        name.DefaultValidate(
                PetConstraints.MinNameLength,
                PetConstraints.MaxNameLength)
            .AddErrorIfFailure(errors);

        description.DefaultValidate(
                PetConstraints.MinDescriptionLength,
                PetConstraints.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Pet(
            id,
            name,
            description,
            petType,
            color,
            healthDetails,
            characteristicsDetails,
            address,
            age,
            supportDetails,
            photosList);
    }
}