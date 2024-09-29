using KindPaws.Domain.Shared.IDs;

namespace KindPaws.Domain.Managements.PetManagement.ValueObjects;

public record PetType
{
    public PetType()
    {
    }

    public PetType(SpecieId specieId, BreedId breedId)
    {
        SpecieId = specieId;
        BreedId = breedId;
    }

    public SpecieId SpecieId { get; }
    public BreedId BreedId { get; }
}