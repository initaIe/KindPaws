using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.HardDeleteSpecie;

public record HardDeleteSpecieExistenceValidationData(Guid SpecieId) : IExistenceValidationData;