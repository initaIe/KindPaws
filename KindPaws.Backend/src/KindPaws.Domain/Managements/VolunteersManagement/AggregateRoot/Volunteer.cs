using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>, ISoftDeleteable
{
    private readonly List<Pet> _pets = [];
    private bool _isDeleted;

    // ef core
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(
        VolunteerId id,
        FullName fullName,
        EmailAddress emailAddress,
        PhoneNumber phoneNumber)
        : base(id)
    {
        FullName = fullName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }

    public FullName FullName { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public MediumDescription? Description { get; private set; }
    public Address? Address { get; private set; }
    public YearsOfExperience? YearsOfExperience { get; private set; }
    public SocialNetworkList SocialNetworkList { get; private set; } = new([]);
    public RequisiteList RequisiteList { get; private set; } = new([]);
    public IReadOnlyList<Pet> Pets => _pets;

    public void Delete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public int GetCountPetsAlreadyFoundHome()
    {
        return _pets.Count(x => x.SupportStatus == SupportStatus.AlreadyFoundHome);
    }

    public int GetCountPetsLookingHome()
    {
        return _pets.Count(x => x.SupportStatus == SupportStatus.LookingHome);
    }

    public int GetCountPetsNeedHelp()
    {
        return _pets.Count(x => x.SupportStatus == SupportStatus.NeedSupport);
    }

    public void UpdateMainInfo(
        FullName fullName,
        EmailAddress emailAddress,
        PhoneNumber phoneNumber)
    {
        FullName = fullName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }

    public void UpdateAdditionalInfo(
        MediumDescription? description,
        Address? address,
        YearsOfExperience? yearsOfExperience,
        SocialNetworkList? socialNetworkList,
        RequisiteList? requisiteList)
    {
        Description = description;
        Address = address;
        YearsOfExperience = yearsOfExperience;
        SocialNetworkList = socialNetworkList ?? new SocialNetworkList([]);
        RequisiteList = requisiteList ?? new RequisiteList([]);
    }
}