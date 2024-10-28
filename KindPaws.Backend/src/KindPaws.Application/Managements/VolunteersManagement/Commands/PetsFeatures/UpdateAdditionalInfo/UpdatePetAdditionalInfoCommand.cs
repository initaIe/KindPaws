using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;

public record UpdatePetAdditionalInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    string? SupportStatus,
    string? Description,
    string? Color,
    DateOnly? BirthDate,
    HealthDetailsDTO? HealthDetails,
    BiometricDetailsDTO? BiometricDetails)
    : ICommand
{
    public UpdatePetAdditionalInfoExistenceCheckData ToExistenceCheckData()
    {
        return new UpdatePetAdditionalInfoExistenceCheckData(VolunteerId, PetId);
    }
}