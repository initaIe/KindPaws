using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.Entities;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Helpers;

public static class PetHelper
{
    public static Pet ForceCreateNewPet(string name, Guid specieId, Guid breedId)
    {
        var petName = PetName.Create(name).Value;
        var petSpecieId = SpecieId.Create(specieId).Value;
        var petType = PetType.Create(petSpecieId, breedId).Value;

        return Pet.CreateNew(
            petName,
            petType);
    }
}