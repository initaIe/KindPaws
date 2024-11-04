using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.Entities;

namespace KindPaws.Species.Application.Helpers;

public static class BreedHelper
{
    public static Breed ForceCreateNewBreed(string name, string description)
    {
        var breedId = BreedId.CreateRandom();
        var breedName = ShortName.Create(name).Value;
        var breedDescription = MediumDescription.Create(description).Value;

        return new Breed(
            breedId,
            breedName,
            breedDescription);
    }
}