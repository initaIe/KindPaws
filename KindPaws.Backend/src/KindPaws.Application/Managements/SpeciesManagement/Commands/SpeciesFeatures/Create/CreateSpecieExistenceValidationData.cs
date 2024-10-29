using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;

public record CreateSpecieExistenceValidationData(string Name)
    : IExistenceValidationData;