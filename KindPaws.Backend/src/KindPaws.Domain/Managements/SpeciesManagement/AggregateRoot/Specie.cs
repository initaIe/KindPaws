using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;

public class Specie : Entity<SpecieId>, IFullDeletable
{
    private readonly List<Breed> _breeds = [];

    // ef core
    private Specie(SpecieId id) : base(id)
    {
    }

    public Specie(
        SpecieId id,
        ShortName name,
        MediumDescription description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public ShortName Name { get; private set; }
    public MediumDescription Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
    public bool IsSoftDeleted { get; private set; }
    public DateTime? SoftDeletedDateTime { get; private set; }
    public bool IsHardDeleted { get; private set; }

    public void AddBreed(Breed breed)
    {
        _breeds.Add(breed);
    }

    public Result<Error> HardDeleteBreed(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);

        if (breed == null)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(breedId), breedId.Value);

        breed.HardDelete();
        _breeds.Remove(breed);
        return true;
    }

    public Result<Error> SoftDeleteBreed(BreedId breedId)
    {
        var breed = _breeds.FirstOrDefault(b => b.Id == breedId);

        if (breed == null)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(breedId), breedId.Value);

        breed.SoftDelete();
        return true;
    }

    public void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletedDateTime = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletedDateTime = null;
        _breeds.ForEach(breed => breed.Restore());
    }

    public void HardDelete()
    {
        IsHardDeleted = true;
        _breeds.ForEach(breed => breed.HardDelete());
    }
}