using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.Entities;

namespace KindPaws.Species.Domain.AggregateRoot;

public class Specie : Entity<SpecieId>, ISoftDeletable
{
    private readonly List<Breed> _breeds = [];

    // ef core
    private Specie(SpecieId id) : base(id)
    {
    }

    public Specie(
        SpecieId id,
        ShortAlphabeticWhiteSpacesString name,
        MediumString description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public ShortAlphabeticWhiteSpacesString Name { get; private set; }
    public MediumString Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
    public bool IsSoftDeleted { get; private set; }
    public UtcNowTimestamp? SoftDeletionTimestamp { get; private set; }

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
        var breed = _breeds.FirstOrDefault(b=>b.Id == breedId);
        
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
        SoftDeletionTimestamp = UtcNowTimestamp.CreateNew();
        _breeds.ForEach(breed => breed.SoftDelete());
    }

    public void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletionTimestamp = null;
        _breeds.ForEach(breed => breed.Restore());
    }
}