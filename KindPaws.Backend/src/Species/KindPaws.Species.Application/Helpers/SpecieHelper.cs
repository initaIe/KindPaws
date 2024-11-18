using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.AggregateRoot;

namespace KindPaws.Species.Application.Helpers;

public static class SpecieHelper
{
    public static Specie ForceCreateNewSpecie(string name, string description)
    {
        var specieId = SpecieId.CreateRandom();
        var specieName = ShortAlphabeticWhiteSpacesString.Create(name).Value;
        var specieDescription = MediumString.Create(description).Value;

        return new Specie(
            specieId,
            specieName,
            specieDescription);
    }
}