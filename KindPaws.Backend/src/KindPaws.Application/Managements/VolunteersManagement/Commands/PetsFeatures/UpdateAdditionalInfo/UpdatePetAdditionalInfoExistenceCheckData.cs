using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;

public record UpdatePetAdditionalInfoExistenceCheckData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceCheckData;