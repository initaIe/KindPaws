using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;

public class Specie : Entity<SpecieId>
{
    private readonly List<Breed> _breeds;

    private Specie(SpecieId id) : base(id)
    {
    }

    public Specie(
        SpecieId id,
        IEnumerable<Breed> breeds,
        ShortName name,
        MediumDescription description)
        : base(id)
    {
        _breeds = breeds.ToList();
        Name = name;
        Description = description;
    }

    public ShortName Name { get; private set; }
    public MediumDescription Description { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
}