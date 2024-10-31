using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.SoftDelete;

public record SoftDeleteSpecieExistenceValidationData(Guid SpecieId) : IExistenceValidationData;