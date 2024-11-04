using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.SoftDelete;

public record SoftDeleteSpecieCommand(Guid SpecieId) : ICommand
{
    public SoftDeleteSpecieExistenceValidationData ToExistenceValidationData()
        => new(SpecieId);
}