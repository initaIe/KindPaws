using KindPaws.Pets.Domain.VolunteersManagement.Entities;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Pets.Domain.VolunteersManagement.AggregateRoot;

// TODO: add edit requisite/pet
public class Volunteer : AggregateRoot<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    private List<Requisite> _requisites = [];

    // ef core
    private Volunteer(
        VolunteerId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    public VolunteerDescription? Description { get; private set; }
    public YearsOfExperience? YearsOfExperience { get; private set; }
    public IReadOnlyList<Requisite> Requisites => _requisites;
    public IReadOnlyList<Pet> Pets => _pets;

    #region Properties

    public int GetCountPetsAlreadyFoundHome()
        => _pets.Count(x => x.SupportStatus == SupportStatus.AlreadyFoundHome);

    public int GetCountPetsLookingHome()
        => _pets.Count(x => x.SupportStatus == SupportStatus.LookingHome);

    public int GetCountPetsNeedHelp()
        => _pets.Count(x => x.SupportStatus == SupportStatus.NeedSupport);

    #endregion

    #region Factory methods

    public static Volunteer CreateNew()
    {
        var id = VolunteerId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Volunteer(
            id,
            createdAt);
    }

    public static Volunteer Create(
        VolunteerId id,
        CreatedAt createdAt)
    {
        return new Volunteer(
            id,
            createdAt);
    }

    #endregion

    #region Pets CRUD

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId);

        if (pet == null)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId), petId.Value);

        return pet;
    }

    public bool HasPetById(PetId petId)
    {
        return _pets.Any(p => p.Id == petId);
    }

    public Result<Error> AddPet(Pet pet)
    {
        var isPetAlreadyExist = HasPetById(pet.Id);

        if (isPetAlreadyExist)
            return Errors.General.RecordAlreadyExist(nameof(Pet), nameof(PetId));

        _pets.Add(pet);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> AddPets(IEnumerable<Pet> pets)
    {
        foreach (var pet in pets)
        {
            var addPetResult = AddPet(pet);

            if (addPetResult.IsFailure)
                return addPetResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeletePet(PetId petId)
    {
        var getPetResult = GetPetById(petId);

        if (getPetResult.IsFailure)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId));

        _pets.Remove(getPetResult.Value);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeletePets(IEnumerable<PetId> petIds)
    {
        foreach (var petId in petIds)
        {
            var deletePetResult = DeletePet(petId);

            if (deletePetResult.IsFailure)
                return deletePetResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> UpdatePetMainInfo(
        PetId petId,
        PetName name,
        PetType type)
    {
        var getPetResult = GetPetById(petId);

        if (getPetResult.IsFailure)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId));

        getPetResult.Value.UpdateMainInfo(name, type);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> UpdatePetsMainInfo(IEnumerable<(PetId petId, PetName name, PetType type)> updatePetsMainInfo)
    {
        foreach (var updatePetMainInfo in updatePetsMainInfo)
        {
            var getPetResult = GetPetById(updatePetMainInfo.petId);

            if (getPetResult.IsFailure)
                return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId));

            getPetResult.Value.UpdateMainInfo(updatePetMainInfo.name, updatePetMainInfo.type);
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> UpdatePetOtherInfo(
        PetId petId,
        PetDescription? description,
        BirthdayAt birthdayAt,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails)
    {
        var getPetResult = GetPetById(petId);

        if (getPetResult.IsFailure)
            return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId));

        getPetResult.Value.UpdateOtherInfo(
            description,
            birthdayAt,
            healthDetails, 
            biometricDetails);
        UpdateLastModifiedAt();
        return true;
    }
    
    public Result<Error> UpdatePetsOtherInfo(
        IEnumerable<(PetId petId,
        PetDescription? description,
        BirthdayAt birthdayAt,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails)> updatePetsOtherInfo)
    {
        foreach (var updatePetOtherInfo in updatePetsOtherInfo)
        {
            var getPetResult = GetPetById(updatePetOtherInfo.petId);

            if (getPetResult.IsFailure)
                return Errors.General.RecordNotFound(nameof(Pet), nameof(PetId));

            getPetResult.Value.UpdateOtherInfo(
                updatePetOtherInfo.description,
                updatePetOtherInfo.birthdayAt,
                updatePetOtherInfo.healthDetails, 
                updatePetOtherInfo.biometricDetails);
        }

        UpdateLastModifiedAt();
        return true;
    }

    #endregion

    #region Requisites CRUD

    public bool HasRequisite(Requisite requisite)
    {
        return _requisites.Any(r => r == requisite);
    }

    public Result<Error> AddRequisite(Requisite requisite)
    {
        var isRequisiteExist = HasRequisite(requisite);

        if (isRequisiteExist)
            return Errors.General.RecordAlreadyExist(nameof(Requisite));

        _requisites.Add(requisite);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> AddRequisites(IEnumerable<Requisite> requisites)
    {
        foreach (var requisite in requisites)
        {
            var addRequisiteResult = AddRequisite(requisite);

            if (addRequisiteResult.IsFailure)
                return addRequisiteResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteRequisite(Requisite requisite)
    {
        var isRequisiteExist = HasRequisite(requisite);

        if (!isRequisiteExist)
            return Errors.General.RecordNotFound(nameof(Requisite));

        _requisites.Remove(requisite);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteRequisites(IEnumerable<Requisite> requisites)
    {
        foreach (var requisite in requisites)
        {
            var deleteRequisiteResult = DeleteRequisite(requisite);

            if (deleteRequisiteResult.IsFailure)
                return deleteRequisiteResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    #endregion

    #region Volunteer CRUD

    public void UpdateInfo(
        VolunteerDescription? description,
        YearsOfExperience? yearsOfExperience)
    {
        Description = description;
        YearsOfExperience = yearsOfExperience;
        UpdateLastModifiedAt();
    }

    #endregion
}