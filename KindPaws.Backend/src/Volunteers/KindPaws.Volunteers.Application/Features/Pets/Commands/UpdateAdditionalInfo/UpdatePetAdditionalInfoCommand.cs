using KindPaws.Core.Abstractions.Markers;
using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateAdditionalInfo;

public record UpdatePetAdditionalInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    string? SupportStatus,
    string? Description,
    string? Color,
    DateOnly? BirthDate,
    HealthDetailsDto? HealthDetails,
    BiometricDetailsDto? BiometricDetails)
    : ICommand
{
    public UpdatePetAdditionalInfoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId, PetId);
}