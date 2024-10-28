using KindPaws.Domain.Managements.SpeciesManagement.AggregateRoot;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Helpers;

public static class SpecieHelper
{
    public static Specie ForceCreateNewSpecie(string name, string description)
    {
        var specieId = SpecieId.CreateRandom();
        var specieName = ShortName.Create(name).Value;
        var specieDescription = MediumDescription.Create(description).Value;

        return new Specie(
            specieId,
            specieName,
            specieDescription);
    }
}