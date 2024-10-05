using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.SpeciesManagement.Entities;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {
    }

    public Breed(
        BreedId id,
        ShortName name,
        MediumDescription description)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public ShortName Name { get; private set; }
    public MediumDescription Description { get; private set; }
}