using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.DeletePhotos;

public record DeletePetPhotosExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;