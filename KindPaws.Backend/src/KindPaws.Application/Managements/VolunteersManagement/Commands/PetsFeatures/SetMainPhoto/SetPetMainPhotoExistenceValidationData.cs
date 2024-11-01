using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SetMainPhoto;

public record SetPetMainPhotoExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;