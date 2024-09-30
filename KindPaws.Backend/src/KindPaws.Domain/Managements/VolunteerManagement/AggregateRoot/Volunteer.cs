using KindPaws.Domain.Managements.VolunteerManagement.Constraints;
using KindPaws.Domain.Managements.VolunteerManagement.Entities;
using KindPaws.Domain.Managements.VolunteerManagement.ValueObjects;
using KindPaws.Domain.Managements.VolunteerManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteerManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>
{
    private readonly List<Pet> _pets;

    public Volunteer(VolunteerId id) : base(id)
    {
    }

    private Volunteer(
        VolunteerId id,
        List<Pet> pets,
        FullName fullName,
        EmailAddress emailAddress,
        string description,
        int experience,
        PhoneNumber phoneNumber,
        SocialNetworkList socialNetworks)
        : base(id)
    {
        _pets = pets;
        FullName = fullName;
        EmailAddress = emailAddress;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
    }

    public FullName FullName { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public string Description { get; private set; }
    public int Experience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public SocialNetworkList SocialNetworks { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

    public int GetCountPetsAlreadyFoundHome =>
        _pets.Count(x => x.SupportDetails.Status == SupportStatus.AlreadyFoundHome);

    public int GetCountPetsLookingHome =>
        _pets.Count(x => x.SupportDetails.Status == SupportStatus.LookingHome);

    public int GetCountPetsNeedHelp =>
        _pets.Count(x => x.SupportDetails.Status == SupportStatus.NeedSupport);

    public static Result<Volunteer, IEnumerable<string>> Create(
        VolunteerId id,
        List<Pet> pets,
        FullName fullName,
        EmailAddress emailAddress,
        string description,
        int experience,
        PhoneNumber phoneNumber,
        SocialNetworkList socialNetworks)
    {
        List<string> errors = [];

        description.DefaultValidate(
                VolunteerConstraints.MinDescriptionLength,
                VolunteerConstraints.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (experience < VolunteerConstraints.MinExperienceValue)
            errors.Add("Experience can not be smaller than 0.");

        if (errors.Count > 0)
            return errors;

        return new Volunteer(
            id,
            pets,
            fullName,
            emailAddress,
            description,
            experience,
            phoneNumber,
            socialNetworks);
    }
}