using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.Entities;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Domain.AggregateRoot;

public class Volunteer : IEntity<VolunteerId>, ISoftDeletable
{
    private readonly List<Pet> _pets = [];
    private List<Requisite> _requisites;

    // ef core
    private Volunteer()
    {
    }

    public Volunteer(
        VolunteerId id,
        MediumString? description,
        Address? address,
        YearsOfExperience? yearsOfExperience,
        IEnumerable<Requisite> requisites)
    {
        Id = id;
        Description = description;
        Address = address;
        YearsOfExperience = yearsOfExperience;
        _requisites = requisites.ToList();
    }

    public VolunteerId Id { get; }
    public MediumString? Description { get; private set; }
    public Address? Address { get; private set; }
    public YearsOfExperience? YearsOfExperience { get; private set; }
    public IReadOnlyList<Requisite> Requisites => _requisites;
    public IReadOnlyList<Pet> Pets => _pets;
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletionTimestamp { get; private set; }

    public int GetCountPetsAlreadyFoundHome()
        => _pets.Count(x => x.SupportStatus == SupportStatus.AlreadyFoundHome);

    public int GetCountPetsLookingHome()
        => _pets.Count(x => x.SupportStatus == SupportStatus.LookingHome);

    public int GetCountPetsNeedHelp()
        => _pets.Count(x => x.SupportStatus == SupportStatus.NeedSupport);


    public void UpdateInfo(
        MediumString? description,
        Address? address,
        YearsOfExperience? yearsOfExperience,
        IEnumerable<Requisite> requisites)
    {
        Description = description;
        Address = address;
        YearsOfExperience = yearsOfExperience;
        _requisites = requisites.ToList();
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

    public Result<Error> AddPetPhotos(
        PetId petId,
        IEnumerable<PetPhoto> photos)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        petResult.Value.AddPhotos(photos);
        return true;
    }

    public Result<Error> DeletePetPhotos(
        PetId petId,
        IEnumerable<PetPhoto> photos)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        petResult.Value.DeletePhotos(photos);
        return true;
    }

    public Result<Error> SetPetMainPhoto(
        PetId petId,
        FilePath photoFilePath)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        petResult.Value.SetMainPhoto(photoFilePath);
        return true;
    }

    public Result<Error> UpdatePetMainInfo(
        PetId petId,
        PetType petType,
        ShortString name)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        petResult.Value.UpdateMainInfo(petType, name);
        return true;
    }

    public Result<Error> UpdatePetAdditionalInfo(
        PetId petId,
        SupportStatus? supportStatus,
        MediumString? description,
        PetColor? petColor,
        Birthday? age,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails)
    {
        var petResult = GetPetById(petId);
        if (petResult.IsFailure)
            return petResult.Error;

        petResult.Value.UpdateAdditionalInfo(
            supportStatus,
            description,
            petColor,
            age,
            healthDetails,
            biometricDetails);
        return true;
    }

    public void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletionTimestamp = DateTime.UtcNow;
        _pets.ForEach(p => p.SoftDelete());
    }

    public void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletionTimestamp = null;
        _pets.ForEach(p => p.Restore());
    }

    public void HardDeletePet(PetId petId)
    {
        var petResult = GetPetById(petId);

        if (petResult.IsFailure)
            return;

        _pets.Remove(petResult.Value);
        AlignPetPositionsAfterDelete(petResult.Value.Position);
    }

    public void SoftDeletePet(PetId petId)
    {
        var petResult = GetPetById(petId);

        if (petResult.IsFailure)
            return;

        petResult.Value.SoftDelete();
        AlignPetPositionsAfterDelete(petResult.Value.Position);
    }

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
        var petResult = GetPetById(petId);

        if (petResult.IsFailure)
            return petResult.Error;

        var movablePet = petResult.Value;

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
}