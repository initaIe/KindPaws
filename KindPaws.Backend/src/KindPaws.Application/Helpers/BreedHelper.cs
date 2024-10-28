using KindPaws.Domain.Managements.SpeciesManagement.Entities;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Helpers;

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