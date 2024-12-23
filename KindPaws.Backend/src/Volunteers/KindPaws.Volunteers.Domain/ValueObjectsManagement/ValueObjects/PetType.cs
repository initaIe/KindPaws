using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record PetType
{
    private PetType(SpecieId specieId, Guid breedId)
    {
        SpecieId = specieId;
        BreedId = breedId;
    }

    public SpecieId SpecieId { get; }
    public Guid BreedId { get; }

    public static Result<PetType, Error> Create(SpecieId specieId, Guid breedId)
    {
        if (GuidValidator.IsEmpty(breedId))
            return ErrorsGeneral.ValueIsInvalid(nameof(breedId));

        return new PetType(specieId, breedId);
    }
}