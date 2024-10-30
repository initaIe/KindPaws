using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Delete;

public record DeleteSpecieCommand(Guid SpecieId) : ICommand
{
    public DeleteSpecieExistenceValidationData ToExistenceValidationData()
        => new(SpecieId);
}