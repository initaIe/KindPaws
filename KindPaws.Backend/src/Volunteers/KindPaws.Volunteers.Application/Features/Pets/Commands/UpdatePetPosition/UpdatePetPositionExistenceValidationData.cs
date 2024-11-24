using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetPosition;

public record UpdatePetPositionExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;