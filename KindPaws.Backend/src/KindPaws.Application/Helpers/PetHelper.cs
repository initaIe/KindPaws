using KindPaws.Domain.Managements.VolunteersManagement.Entities;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Helpers;

public static class PetHelper
{
    public static Pet ForceCreateNewPet(string name, Guid specieId, Guid breedId)
    {
        var petId = PetId.CreateRandom();
        var petSpecieId = SpecieId.Create(specieId).Value;
        var petType = new PetType(petSpecieId, breedId);
        var petName = ShortName.Create(name).Value;

        return new Pet(
            petId,
            petType,
            petName,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
    }
}