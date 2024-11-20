using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.Entities;

namespace KindPaws.Species.Domain.AggregateRoot;

public class Specie : IEntity<SpecieId>, ISoftDeletable
{
    private readonly List<Breed> _breeds = [];

    // ef core
    private Specie()
    {
    }

    public Specie(
        SpecieId id,
        ShortAlphabeticWhiteSpacesString name,
        MediumString description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public SpecieId Id { get; }
    public ShortAlphabeticWhiteSpacesString Name { get; private set; }
    public MediumString Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletionTimestamp { get; private set; }

    public void AddBreed(Breed breed)
    {
        _breeds.Add(breed);
    }

    public void HardDeleteBreed(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);

        if (breed == null)
            return;

        _breeds.Remove(breed);
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
        var breedResult = GetBreedById(breedId);

        if (breedResult.IsFailure)
            return;

        breedResult.Value.SoftDelete();
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
}