using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.HardDelete;

public record HardDeleteSpecieExistenceValidationData(Guid SpecieId) : IExistenceValidationData;