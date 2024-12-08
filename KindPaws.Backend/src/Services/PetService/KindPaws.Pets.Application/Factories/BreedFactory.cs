using KindPaws.Pets.Domain.SpeciesManagement.Entities;
using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Pets.Application.Factories;

public static class BreedFactory
{
    public static Breed ForceCreateNew(
        string name,
        string description)
    {
        var breedName = BreedName.Create(name).Value;
        var breedDescription = BreedDescription.Create(description).Value;

        return Breed.CreateNew(breedName, breedDescription);
    }
}