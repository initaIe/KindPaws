using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public record AddPetPhotosExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;