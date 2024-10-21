using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.SpeciesManagement.Entities;

public class Breed : Entity<BreedId>, ISoftDeleteable
{
    private bool _isDeleted;

    // ef core
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

    public ShortName Name { get; private set; } // TODO: create new VO for breed name with alphabetic with white spaces
    public MediumDescription Description { get; private set; }

    public void Delete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }
}