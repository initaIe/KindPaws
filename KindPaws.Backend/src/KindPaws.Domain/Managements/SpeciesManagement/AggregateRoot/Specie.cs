using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;

public class Specie : Entity<SpecieId>, ISoftDeleteable
{
    private bool _isDeleted;
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

    public void Delete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public void AddBreed(Breed breed)
    {
        _breeds.Add(breed);
    }

    public Result<Breed, Error> GetBreedByGuid(Guid breedGuid)
    {
        var breedId = BreedId.Create(breedGuid);

        if (breedId.IsFailure)
            return Errors.General.ValueIsInvalid(nameof(breedId));

        var breed = _breeds.FirstOrDefault(x => x.Id == breedId.Value);

        if (breed == null)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(BreedId), breedId.Value);

        return breed;
    }

    public Result<Breed, Error> GetBreedByName(ShortName name)
    {
        var breed = _breeds.FirstOrDefault(x => x.Name == name);

        if (breed == null)
            return Errors.General.RecordNotFound(nameof(Breed), nameof(ShortName), name.Value);

        return breed;
    }
}