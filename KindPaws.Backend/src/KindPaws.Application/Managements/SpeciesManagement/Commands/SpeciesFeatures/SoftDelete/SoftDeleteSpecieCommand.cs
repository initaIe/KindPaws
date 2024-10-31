using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.SoftDelete;

public record SoftDeleteSpecieCommand(Guid SpecieId) : ICommand
{
    public SoftDeleteSpecieExistenceValidationData ToExistenceValidationData()
        => new(SpecieId);
}