using KindPaws.Application.Abstractions;
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
    : ICommand;