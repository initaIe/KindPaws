using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.HardDeletePet;

public record HardDeletePetExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;