using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.Create;

public record CreateSpecieExistenceValidationData(string Name)
    : IExistenceValidationData;