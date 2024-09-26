using KindPaws.Domain.Managements.PetManagement.AggregateRoot;
using KindPaws.Domain.Managements.PetManagement.Enums;
using KindPaws.Domain.Managements.VolunteerManagement.ValidationRules;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.VOs;
using KindPaws.Domain.Shared.VOs.Constraints;

namespace KindPaws.Domain.Managements.VolunteerManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>
{
    private readonly List<Requisite> _helpDetails;
    private readonly List<Pet> _pets;
    private readonly List<SocialNetwork> _socialNetworks;

    public Volunteer(
        VolunteerId id,
        List<Requisite> helpDetails,
        List<Pet> pets,
        List<SocialNetwork> socialNetworks,
        FullName fullName,
        EmailAddress emailAddressAddress,
        DescriptionConstraints description,
        Experience experience,
        PhoneNumber phoneNumber) : base(id)
    {
        _helpDetails = helpDetails;
        _pets = pets;
        _socialNetworks = socialNetworks;
        FullName = fullName;
        EmailAddressAddress = emailAddressAddress;
        Description = description;
        Experience = experience;
        PhoneNumber = phoneNumber;
    }

    public FullName FullName { get; private set; }
    public EmailAddress EmailAddressAddress { get; private set; }
    public DescriptionConstraints Description { get; private set; }

    public Experience Experience { get; private set; }
    public int GetCountPetsAlreadyFoundHome => _pets.Count(x => x.HelpInfo.Status == HelpStatus.AlreadyFoundHome);
    public int GetCountPetsLookingHome => _pets.Count(x => x.HelpInfo.Status == HelpStatus.LookingHome);
    public int GetCountPetsNeedHelp => _pets.Count(x => x.HelpInfo.Status == HelpStatus.NeedHelp);
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Requisite> HelpDetails => _helpDetails;
    public IReadOnlyList<Pet> Pets => _pets;

    public static Result<Volunteer, IEnumerable<string>> Create(
        VolunteerId id,
        List<Requisite> helpDetails,
        List<Pet> pets,
        List<SocialNetwork> socialNetworks,
        FullName fullName,
        EmailAddress emailAddressAddress,
        DescriptionConstraints description,
        Experience experience,
        PhoneNumber phoneNumber)
    {
        var volunteer = new Volunteer(
            id,
            helpDetails,
            pets,
            socialNetworks,
            fullName,
            emailAddressAddress,
            description,
            experience,
            phoneNumber);

        return volunteer;
    }
}