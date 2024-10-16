using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.PetHandlers.UpdatePhotos;

namespace KindPaws.API.Contracts.Volunteers;

public record UpdatePetPhotosRequest(
    IFormFileCollection Photos);