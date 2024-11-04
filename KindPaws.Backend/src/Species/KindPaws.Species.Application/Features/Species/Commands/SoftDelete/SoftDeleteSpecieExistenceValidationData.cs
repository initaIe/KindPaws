using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.SoftDelete;

public record SoftDeleteSpecieExistenceValidationData(Guid SpecieId) : IExistenceValidationData;