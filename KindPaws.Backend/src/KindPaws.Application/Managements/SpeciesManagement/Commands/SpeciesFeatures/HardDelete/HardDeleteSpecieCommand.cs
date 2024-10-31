using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.SoftDelete;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.HardDelete;

public record HardDeleteSpecieCommand(Guid SpecieId) : ICommand
{
    public HardDeleteSpecieExistenceValidationData ToExistenceValidationData()
        => new(SpecieId);
}