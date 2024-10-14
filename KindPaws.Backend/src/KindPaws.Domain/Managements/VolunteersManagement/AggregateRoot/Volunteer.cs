using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>, ISoftDeleteable
{
    private bool _isDeleted;
    private readonly List<Pet> _pets = [];
    
    private List<Requisite> _requisites;
    private List<SocialNetwork> _socialNetworks;

    // ef core
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(
        VolunteerId id,
        IEnumerable<SocialNetwork>? socialNetworks,
        IEnumerable<Requisite>? requisites,
        FullName fullName,
        EmailAddress emailAddress,
        PhoneNumber phoneNumber,
        MediumDescription? description,
        Address? address,
        YearsOfExperience? yearsOfExperience)
        : base(id)
    {
        _socialNetworks = socialNetworks?.ToList() ?? [];
        _requisites = requisites?.ToList() ?? [];
        FullName = fullName;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Description = description;
        Address = address;
        YearsOfExperience = yearsOfExperience;
    }

    public FullName FullName { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public MediumDescription? Description { get; private set; }
    public Address? Address { get; private set; }
    public YearsOfExperience? YearsOfExperience { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Requisite> Requisites => _requisites;
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
        IEnumerable<SocialNetwork>? socialNetworks,
        IEnumerable<Requisite>? requisites)
    {
        Description = description;
        Address = address;
        YearsOfExperience = yearsOfExperience;
        _socialNetworks = socialNetworks?.ToList() ?? [];
        _requisites = requisites?.ToList() ?? [];
    }

    public void AddPet(Pet pet)
    {
        _pets.Add(pet);
    }

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId);
        
        if (pet == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), petId.Value);

        return pet;
    }
}