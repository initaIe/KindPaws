using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.HardDelete;

public record HardDeletePetExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;