using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record PetType
{
    public PetType(SpecieId specieId, Guid breedId)
    {
        SpecieId = specieId;
        BreedId = breedId;
    }

    public SpecieId SpecieId { get; }
    public Guid BreedId { get; }
}