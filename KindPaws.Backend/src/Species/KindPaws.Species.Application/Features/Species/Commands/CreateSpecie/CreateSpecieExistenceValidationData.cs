using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.CreateSpecie;

public record CreateSpecieExistenceValidationData(string Name)
    : IExistenceValidationData;