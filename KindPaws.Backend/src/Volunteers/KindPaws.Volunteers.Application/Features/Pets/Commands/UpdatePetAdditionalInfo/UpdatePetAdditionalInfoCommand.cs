using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetAdditionalInfo;

public record UpdatePetAdditionalInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    string? SupportStatus,
    string? Description,
    string? Color,
    DateTimeOffset? Birthday,
    HealthDetailsDto? HealthDetails,
    BiometricDetailsDto? BiometricDetails)
    : ICommand;