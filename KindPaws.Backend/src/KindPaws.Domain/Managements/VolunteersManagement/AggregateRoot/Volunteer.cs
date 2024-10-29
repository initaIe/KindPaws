using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;

public class Volunteer : Entity<VolunteerId>, ISoftDeleteable
{
    private readonly List<Pet> _pets = [];
    private bool _isDeleted;
    private List<Requisite> _requisites = [];
    private List<SocialNetwork> _socialNetworks = [];

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

    // TODO: refactor position methods
    public Result<Error> AddPet(Pet pet)
    {
        var lastPositionNumber = _pets.Count + Position.ChangeUnit;

        var positionResult = Position.Create(lastPositionNumber);
        if (positionResult.IsFailure)
            return positionResult.Error; // TODO: throw exception mb?

        pet.SetPosition(positionResult.Value);

        _pets.Add(pet);
        return true;
    }

    public Result<Error> DeletePet(Pet pet)
    {
        _pets.Remove(pet);

        var petsToDecreasePosition
            = _pets.Where(p => p.Position.Value > pet.Position.Value);

        foreach (var petToDecreasePosition in petsToDecreasePosition)
        {
            var decreasePositionResult = petToDecreasePosition.DecreasePosition();
            if (decreasePositionResult.IsFailure)
                return decreasePositionResult.Error;
        }

        return true;
    }

    public Result<Error> MovePet(Pet pet, Position position)
    {
        if (pet.Position.Value == position.Value || _pets.Count == 1)
            return true;

        var lastPosition = Position.Create(_pets.Count);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        if (position.Value > lastPosition.Value.Value) position = lastPosition.Value;

        var isIncrease = pet.Position.Value < position.Value;

        if (isIncrease)
        {
            var petsToDecreasePosition = _pets.Where(p =>
                p.Position.Value > pet.Position.Value
                && p.Position.Value <= position.Value);

            foreach (var petToDecreasePosition in petsToDecreasePosition)
            {
                var decreasePositionResult = petToDecreasePosition.DecreasePosition();
                if (decreasePositionResult.IsFailure)
                    return decreasePositionResult.Error;
            }
        }
        else
        {
            var petsToIncreasePosition = _pets.Where(p =>
                p.Position.Value < pet.Position.Value
                && p.Position.Value >= position.Value);

            foreach (var petToIncreasePosition in petsToIncreasePosition)
            {
                var increasePositionResult = petToIncreasePosition.IncreasePosition();
                if (increasePositionResult.IsFailure)
                    return increasePositionResult.Error;
            }
        }

        pet.SetPosition(position);
        return true;
    }

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId);

        if (pet == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), petId.Value);

        return pet;
    }
}