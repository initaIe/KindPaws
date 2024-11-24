using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetAdditionalInfo;

public record UpdatePetAdditionalInfoExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;