using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePhotos;

public record DeletePetPhotosExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;