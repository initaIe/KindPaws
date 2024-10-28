using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public record AddPetPhotosExistenceCheckData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceCheckData;