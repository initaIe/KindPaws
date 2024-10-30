using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Delete;

public record DeleteSpecieExistenceValidationData(Guid SpecieId) : IExistenceValidationData;