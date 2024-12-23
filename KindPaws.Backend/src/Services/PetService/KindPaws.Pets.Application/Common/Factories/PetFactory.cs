using KindPaws.Pets.Domain.VolunteersManagement.Entities;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Pets.Application.Common.Factories;

public static class PetFactory
{
    public static Pet ForceCreateNew(
        string name,
        Guid specieId,
        Guid breedId)
    {
        var petName = PetName.Create(name).Value;

        var petSpecieId = SpecieId.Create(specieId).Value;
        var petType = PetType.Create(petSpecieId, breedId).Value;

        return Pet.CreateNew(petName, petType);
    }
}