using KindPaws.SharedKernel.Others.DeletionManagement;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.Entities;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Domain.AggregateRoot;

public class Volunteer : Entity<VolunteerId>, IFullDeletable
{
    private readonly List<Pet> _pets = [];
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
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletedDateTime { get; private set; }
    public bool IsHardDeleted { get; private set; }

    public int GetCountPetsAlreadyFoundHome()
        => _pets.Count(x => x.SupportStatus == SupportStatus.AlreadyFoundHome);

    public int GetCountPetsLookingHome()
        => _pets.Count(x => x.SupportStatus == SupportStatus.LookingHome);

    public int GetCountPetsNeedHelp()
        => _pets.Count(x => x.SupportStatus == SupportStatus.NeedSupport);


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

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(x => x.Id == petId);

        if (pet == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), petId.Value);

        return pet;
    }

    public Result<Error> AddPet(Pet pet)
    {
        var getLastPositionResult = GeneratePositionForNewPet();
        if (getLastPositionResult.IsFailure)
            return getLastPositionResult.Error;

        pet.UpdatePosition(getLastPositionResult.Value);
        _pets.Add(pet);
        return true;
    }


    #region Deletion methods

    public void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletedDateTime = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletedDateTime = null;
        _pets.ForEach(p => p.Restore());
    }

    public void HardDelete()
    {
        IsHardDeleted = true;
        _pets.ForEach(pet => pet.HardDelete());
    }

    public Result<Error> HardDeletePet(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);

        if (pet == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), petId.Value);

        pet.HardDelete();
        _pets.Remove(pet);

        AlignPetPositionsAfterDelete(pet.Position);

        return true;
    }

    public Result<Error> SoftDeletePet(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);

        if (pet == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), petId.Value);

        pet.SoftDelete();

        AlignPetPositionsAfterDelete(pet.Position);

        return true;
    }

    #endregion

    #region Position methods

    private Result<Error> AlignPetPositionsAfterDelete(Position deletedPetPosition)
    {
        var petsToDecreasePosition = _pets.Where(p => p.Position.Value > deletedPetPosition.Value);

        foreach (var petToDecreasePosition in petsToDecreasePosition)
        {
            var decreasePositionResult = petToDecreasePosition.DecreasePosition();
            if (decreasePositionResult.IsFailure)
                return decreasePositionResult.Error;
        }

        return true;
    }

    private Result<Position, Error> GeneratePositionForNewPet()
    {
        var lastPositionNumber = _pets.Count + Position.ChangeUnit;
        var positionResult = Position.Create(lastPositionNumber);
        return positionResult;
    }

    public Result<Error> MovePet(PetId petId, Position newPosition)
    {
        var movablePet = _pets.FirstOrDefault(p => p.Id == petId);
        if (movablePet == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), petId.Value);

        if (movablePet.Position.Value == newPosition.Value || _pets.Count == 1)
            return true;

        var lastPosition = Position.Create(_pets.Count);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        if (newPosition.Value > lastPosition.Value.Value)
            newPosition = lastPosition.Value;

        var isIncrease = movablePet.Position.Value < newPosition.Value;

        if (isIncrease)
            DecreasePetsPositionsWhenMovePet(movablePet, newPosition);
        else
            IncreasePetsPositionsWhenMovePet(movablePet, newPosition);

        movablePet.UpdatePosition(newPosition);
        return true;
    }

    private Result<Error> DecreasePetsPositionsWhenMovePet(Pet movablePet, Position newPosition)
    {
        var petsToDecreasePosition = _pets.Where(p =>
            p.Position.Value > movablePet.Position.Value
            && p.Position.Value <= newPosition.Value);

        foreach (var petToDecreasePosition in petsToDecreasePosition)
        {
            var decreasePositionResult = petToDecreasePosition.DecreasePosition();
            if (decreasePositionResult.IsFailure)
                return decreasePositionResult.Error;
        }

        return true;
    }

    private Result<Error> IncreasePetsPositionsWhenMovePet(Pet movablePet, Position newPosition)
    {
        var petsToIncreasePosition = _pets.Where(p =>
            p.Position.Value < movablePet.Position.Value
            && p.Position.Value >= newPosition.Value);

        foreach (var petToIncreasePosition in petsToIncreasePosition)
        {
            var increasePositionResult = petToIncreasePosition.IncreasePosition();
            if (increasePositionResult.IsFailure)
                return increasePositionResult.Error;
        }

        return true;
    }

    #endregion
}