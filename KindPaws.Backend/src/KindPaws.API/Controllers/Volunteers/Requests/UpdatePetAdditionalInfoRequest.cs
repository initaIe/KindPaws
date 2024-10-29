using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record UpdatePetAdditionalInfoRequest(
    string? SupportStatus,
    string? Description,
    string? Color,
    DateOnly? BirthDate,
    HealthDetailsDTO? HealthDetails,
    BiometricDetailsDTO? BiometricDetails)
{
    public UpdatePetAdditionalInfoCommand ToCommand(Guid id, Guid petId)
        => new(id, petId, SupportStatus, Description, Color, BirthDate, HealthDetails, BiometricDetails);
}