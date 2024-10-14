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
}