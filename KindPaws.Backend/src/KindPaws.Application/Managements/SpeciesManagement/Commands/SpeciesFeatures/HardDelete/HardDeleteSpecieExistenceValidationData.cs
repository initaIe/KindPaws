using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.HardDelete;

public record HardDeleteSpecieExistenceValidationData(Guid SpecieId) : IExistenceValidationData;