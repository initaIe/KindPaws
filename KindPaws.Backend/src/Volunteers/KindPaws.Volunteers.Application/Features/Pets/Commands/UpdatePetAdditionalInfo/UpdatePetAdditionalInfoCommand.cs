using KindPaws.Core.Abstractions.Markers;
using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetAdditionalInfo;

public record UpdatePetAdditionalInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    string? SupportStatus,
    string? Description,
    string? Color,
    DateTime? Birthday,
    HealthDetailsDto? HealthDetails,
    BiometricDetailsDto? BiometricDetails)
    : ICommand;