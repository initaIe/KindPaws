using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.AddPetPhotos;

public record AddPetPhotosExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;