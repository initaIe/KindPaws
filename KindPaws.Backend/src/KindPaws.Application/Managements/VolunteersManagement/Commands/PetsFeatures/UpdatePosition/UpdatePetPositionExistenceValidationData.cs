using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdatePosition;

public record UpdatePetPositionExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;