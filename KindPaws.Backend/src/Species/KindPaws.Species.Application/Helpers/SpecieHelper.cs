using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.AggregateRoot;

namespace KindPaws.Species.Application.Helpers;

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