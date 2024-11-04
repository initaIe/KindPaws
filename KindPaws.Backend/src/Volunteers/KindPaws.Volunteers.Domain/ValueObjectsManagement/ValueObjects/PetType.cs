using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

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