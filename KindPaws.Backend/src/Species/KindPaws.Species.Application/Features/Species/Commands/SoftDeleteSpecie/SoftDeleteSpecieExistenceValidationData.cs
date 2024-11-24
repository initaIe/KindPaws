using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Species.Application.Features.Species.Commands.SoftDeleteSpecie;

public record SoftDeleteSpecieExistenceValidationData(Guid SpecieId) : IExistenceValidationData;