using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePetPhotos;

public record DeletePetPhotosExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;