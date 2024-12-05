using KindPaws.Pets.Domain.SpeciesManagement.Entities;
using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;

public class Specie : AggregateRoot<SpecieId>
{
    private readonly List<Breed> _breeds = [];

    // ef core
    private Specie(
        SpecieId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    private Specie(
        SpecieId id,
        CreatedAt createdAt,
        SpecieName name,
        SpecieDescription description)
        : base(id, createdAt)
    {
        Name = name;
        Description = description;
    }

    public SpecieName Name { get; private set; }
    public SpecieDescription Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    #region Factory methods

    public static Specie CreateNew(
        SpecieName name,
        SpecieDescription description)
    {
        var id = SpecieId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Specie(
            id,
            createdAt,
            name,
            description);
    }

    public static Specie Create(
        SpecieId id,
        CreatedAt createdAt,
        SpecieName name,
        SpecieDescription description)
    {
        return new Specie(
            id,
            createdAt,
            name,
            description);
    }

    #endregion

    #region Breeds CRUD

    public Result<Breed, Error> GetBreedById(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);

        if (breed == null)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), breedId.Value);

        return breed;
    }

    public bool HasBreedById(BreedId breedId)
    {
        return _breeds.Any(b => b.Id == breedId);
    }

    public Result<Error> AddBreed(Breed breed)
    {
        var isBreedAlreadyExist = HasBreedById(breed.Id);

        if (isBreedAlreadyExist)
            return Errors.General.RecordAlreadyExist(nameof(Breed), nameof(BreedId));

        _breeds.Add(breed);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> AddBreeds(IEnumerable<Breed> breeds)
    {
        foreach (var breed in breeds)
        {
            var addBreedResult = AddBreed(breed);

            if (addBreedResult.IsFailure)
                return addBreedResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteBreed(BreedId breedId)
    {
        var getBreedResult = GetBreedById(breedId);

        if (getBreedResult.IsFailure)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(breedId));

        _breeds.Remove(getBreedResult.Value);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteBreeds(IEnumerable<BreedId> breedIds)
    {
        foreach (var breedId in breedIds)
        {
            var deleteBreedResult = DeleteBreed(breedId);

            if (deleteBreedResult.IsFailure)
                return deleteBreedResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> UpdateBreedInfo(
        BreedId breedId,
        BreedName name,
        BreedDescription description)
    {
        var getBreedResult = GetBreedById(breedId);

        if (getBreedResult.IsFailure)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId));

        getBreedResult.Value.UpdateInfo(name, description);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> UpdateBreedsInfo(
        IEnumerable<(BreedId breedId, BreedName name, BreedDescription description)> updateBreedsInfo)
    {
        foreach (var updateBreedInfo in updateBreedsInfo)
        {
            var getBreedResult = GetBreedById(updateBreedInfo.breedId);

            if (getBreedResult.IsFailure)
                return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId));

            getBreedResult.Value.UpdateInfo(updateBreedInfo.name, updateBreedInfo.description);
        }

        UpdateLastModifiedAt();
        return true;
    }

    #endregion

    #region Specie CRUD

    public void UpdateInfo(
        SpecieName name,
        SpecieDescription description)
    {
        Name = name;
        Description = description;
        UpdateLastModifiedAt();
    }

    #endregion
}