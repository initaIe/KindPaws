using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePosition;

public record UpdatePetPositionExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;