using KindPaws.Pets.Domain.SpeciesManagement.AggregateRoot;
using KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Pets.Application.Common.Factories;

public static class SpecieFactory
{
    public static Specie ForceCreateNew(
        string name,
        string description)
    {
        var specieName = SpecieName.Create(name).Value;
        var specieDescription = SpecieDescription.Create(description).Value;

        return Specie.CreateNew(specieName, specieDescription);
    }
}