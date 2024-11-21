using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.Entities;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Species.Application.Helpers;

public static class BreedHelper
{
    public static Breed ForceCreateNewBreed(string name, string description)
    {
        var breedId = BreedId.CreateRandom();
        var breedName = BreedName.Create(name).Value;
        var breedDescription = BreedDescription.Create(description).Value;

        return new Breed(
            breedId,
            breedName,
            breedDescription);
    }
}