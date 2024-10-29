using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public record AddBreedCommand(
    Guid SpecieId,
    string Name,
    string Description)
    : ICommand
{
    public AddBreedExistenceValidationData ToExistenceValidationData()
        => new(SpecieId, Name);
}