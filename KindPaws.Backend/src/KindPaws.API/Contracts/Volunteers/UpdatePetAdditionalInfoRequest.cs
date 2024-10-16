using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.PetHandlers.UpdateAdditionalInfo;

namespace KindPaws.API.Contracts.Volunteers;

public record UpdatePetAdditionalInfoRequest(
    string? SupportStatus,
    string? Description,
    string? PetColor,
    DateOnly? BirthDate,
    HealthDetailsDTO? HealthDetails,
    BiometricDetailsDTO? BiometricDetails)
{
    public UpdatePetAdditionalInfoCommand ToCommand(Guid id, Guid petId)
    {
        return new UpdatePetAdditionalInfoCommand(
            id,
            petId,
            SupportStatus,
            Description,
            PetColor,
            BirthDate,
            HealthDetails,
            BiometricDetails);
    }
}