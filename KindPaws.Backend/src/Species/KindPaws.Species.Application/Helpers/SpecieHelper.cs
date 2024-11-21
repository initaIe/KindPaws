using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Species.Application.Helpers;

public static class SpecieHelper
{
    public static Specie ForceCreateNewSpecie(string name, string description)
    {
        var specieId = SpecieId.CreateRandom();
        var specieName = SpecieName.Create(name).Value;
        var specieDescription = SpecieDescription.Create(description).Value;

        return new Specie(
            specieId,
            specieName,
            specieDescription);
    }
}