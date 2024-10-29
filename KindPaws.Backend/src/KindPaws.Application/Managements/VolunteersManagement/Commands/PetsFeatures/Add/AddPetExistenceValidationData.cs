using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

public record AddPetExistenceValidationData(
    Guid VolunteerId,
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;