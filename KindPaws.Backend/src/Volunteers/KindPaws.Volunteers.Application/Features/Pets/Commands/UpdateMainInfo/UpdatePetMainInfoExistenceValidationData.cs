using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;

public record UpdatePetMainInfoExistenceValidationData(
    Guid VolunteerId,
    Guid PetId,
    Guid SpecieId,
    Guid BreedId)
    : IExistenceValidationData;