using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SoftDeletePet;

public record SoftDeletePetExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;