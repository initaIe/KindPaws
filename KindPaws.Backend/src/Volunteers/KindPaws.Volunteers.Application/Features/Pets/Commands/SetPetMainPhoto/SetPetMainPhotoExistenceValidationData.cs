using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SetPetMainPhoto;

public record SetPetMainPhotoExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;