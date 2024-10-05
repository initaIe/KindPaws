using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(
        VolunteerId id,
        FullName fullName,
        EmailAddress emailAddress,
        MediumDescription? nullableDescription,
        Address? address,
        Experience? experience,
        PhoneNumber phoneNumber,
        SocialNetworkList? socialNetworkList,
        RequisiteList? requisiteList)
        : base(id)
    {
        FullName = fullName;
        EmailAddress = emailAddress;
        Description = nullableDescription ?? MediumDescription.CreateEmpty();
        Address = address ?? Address.CreateEmpty();
        Experience = experience ?? Experience.CreateEmpty();
        PhoneNumber = phoneNumber;
        SocialNetworkList = socialNetworkList ?? new SocialNetworkList([]);
        RequisiteList = requisiteList ?? new RequisiteList([]);
    }

    public FullName FullName { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public MediumDescription Description { get; private set; }
    public Address Address { get; private set; }
    public Experience Experience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public SocialNetworkList SocialNetworkList { get; private set; }
    public RequisiteList RequisiteList { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

    public int GetCountPetsAlreadyFoundHome =>
        _pets.Count(x => x.SupportStatus == SupportStatus.AlreadyFoundHome);

    public int GetCountPetsLookingHome =>
        _pets.Count(x => x.SupportStatus == SupportStatus.LookingHome);

    public int GetCountPetsNeedHelp =>
        _pets.Count(x => x.SupportStatus == SupportStatus.NeedSupport);
}