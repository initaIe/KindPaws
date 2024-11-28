using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.Entities;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Species.Domain.AggregateRoot;

public class Specie : ISoftDeletableEntity<SpecieId>
{
    private readonly List<Breed> _breeds = [];

    // ef core
    private Specie()
    {
    }

    public Specie(
        SpecieId id,
        SpecieName name,
        SpecieDescription description,
        CreationTimestamp creationTimestamp)
    {
        Id = id;
        Name = name;
        Description = description;
        CreationTimestamp = creationTimestamp;
    }

    public SpecieId Id { get; }
    public SpecieName Name { get; private set; }
    public SpecieDescription Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
    public CreationTimestamp CreationTimestamp { get; private set; }
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletionTimestamp { get; private set; }

    #region Factory methods

    public static Specie CreateNew(
        SpecieName name,
        SpecieDescription description)
    {
        var id = SpecieId.CreateRandom();
        var creationTimestamp = CreationTimestamp.CreateNew();

        return new Specie(
            id,
            name,
            description,
            creationTimestamp);
    }

    #endregion

    #region CRUD

    public void AddBreed(Breed breed)
    {
        _breeds.Add(breed);
    }

    public void AddBreeds(IEnumerable<Breed> breeds)
    {
        _breeds.AddRange(breeds);
    }

    public void HardDeleteBreed(BreedId breedId)
    {
        var getBreedResult = GetBreedById(breedId);

        if (getBreedResult.IsFailure)
            return;

        _breeds.Remove(getBreedResult.Value);
    }

    public void HardDeleteBreeds(IEnumerable<BreedId> breedIds)
    {
        foreach (var breedId in breedIds)
        {
            var getBreedResult = GetBreedById(breedId);

            if (getBreedResult.IsFailure)
                continue;

            _breeds.Remove(getBreedResult.Value);
        }
    }

    public Result<Breed, Error> GetBreedById(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);

        if (breed == null)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), breedId);

        return breed;
    }

    public void SoftDeleteBreed(BreedId breedId)
    {
        var getBreedResult = GetBreedById(breedId);

        if (getBreedResult.IsFailure)
            return;

        getBreedResult.Value.SoftDelete();
    }

    public void SoftDeleteBreeds(IEnumerable<BreedId> breedIds)
    {
        foreach (var breedId in breedIds)
        {
            var getBreedResult = GetBreedById(breedId);

            if (getBreedResult.IsFailure)
                continue;

            getBreedResult.Value.SoftDelete();
        }
    }

    public void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletionTimestamp = DateTime.UtcNow;
        _breeds.ForEach(breed => breed.SoftDelete());
    }

    public void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletionTimestamp = null;
        _breeds.ForEach(breed => breed.Restore());
    }

    #endregion
}