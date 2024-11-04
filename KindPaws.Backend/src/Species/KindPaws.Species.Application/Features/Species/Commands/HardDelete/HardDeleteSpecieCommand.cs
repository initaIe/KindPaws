using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.HardDelete;

public record HardDeleteSpecieCommand(Guid SpecieId) : ICommand
{
    public HardDeleteSpecieExistenceValidationData ToExistenceValidationData()
        => new(SpecieId);
}