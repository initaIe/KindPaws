using KindPaws.Application.Abstractions.Markers;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;

public record UpdatePetAdditionalInfoExistenceValidationData(
    Guid VolunteerId,
    Guid PetId)
    : IExistenceValidationData;