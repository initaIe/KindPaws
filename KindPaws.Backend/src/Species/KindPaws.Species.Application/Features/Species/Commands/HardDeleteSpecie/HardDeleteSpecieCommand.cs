using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.HardDeleteSpecie;

public record HardDeleteSpecieCommand(Guid SpecieId) : ICommand
{
    public HardDeleteSpecieExistenceValidationData ToExistenceValidationData()
        => new(SpecieId);
}