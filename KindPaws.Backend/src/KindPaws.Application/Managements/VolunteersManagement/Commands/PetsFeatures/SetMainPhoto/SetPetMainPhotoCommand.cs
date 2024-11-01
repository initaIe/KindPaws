using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SetMainPhoto;

public record SetPetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path)
    : ICommand
{
    public SetPetMainPhotoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}