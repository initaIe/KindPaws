using KindPaws.Core.Dtos;
using KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateAdditionalInfo;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record UpdatePetAdditionalInfoRequest(
    string? SupportStatus,
    string? Description,
    string? Color,
    DateOnly? BirthDate,
    HealthDetailsDto? HealthDetails,
    BiometricDetailsDto? BiometricDetails)
{
    public UpdatePetAdditionalInfoCommand ToCommand(Guid id, Guid petId)
        => new(id, petId, SupportStatus, Description, Color, BirthDate, HealthDetails, BiometricDetails);
}