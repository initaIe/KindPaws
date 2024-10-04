using KindPaws.Domain.Shared.IDs;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record PetType
{
    public PetType(SpecieId specieId, BreedId breedId)
    {
        SpecieId = specieId;
        BreedId = breedId;
    }

    public SpecieId SpecieId { get; }
    public Guid BreedId { get; }
}