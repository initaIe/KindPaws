using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateAdditionalInfo;

public record UpdatePetAdditionalInfoExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;