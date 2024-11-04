using KindPaws.SharedKernel.Others.DeletionManagement;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
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

    public void AddBreed(Breed breed)
    {
        _breeds.Add(breed);
    }

    public void HardDeleteBreed(BreedId breedId)
    {
        var breed = _breeds.SingleOrDefault(b => b.Id == breedId);

        if (breed == null)
            return;

        _breeds.Remove(breed);
    }

    public void SoftDeleteBreed(BreedId breedId)
    {
        var breed = _breeds.SingleOrDefault(b => b.Id == breedId);

        if (breed == null)
            return;

        breed.SoftDelete();
    }

    public void SoftDelete()
    {
        IsSoftDeleted = true;
        SoftDeletedDateTime = DateTime.UtcNow;
        _breeds.ForEach(breed => breed.SoftDelete());
    }

    public void Restore()
    {
        IsSoftDeleted = false;
        SoftDeletedDateTime = null;
        _breeds.ForEach(breed => breed.Restore());
    }
}