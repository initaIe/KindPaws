using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.Entities;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Helpers;

public static class PetHelper
{
    public static Pet ForceCreateNewPet(string name, Guid specieId, Guid breedId)
    {
        var petId = PetId.CreateRandom();
        var petSpecieId = SpecieId.Create(specieId).Value;
        var petType = new PetType(petSpecieId, breedId);
        var petName = PetName.Create(name).Value;

        return new Pet(
            petId,
            petName,
            petType);
    }
}