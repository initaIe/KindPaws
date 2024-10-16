using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.PetHandlers.UpdateAdditionalInfo;

public record UpdatePetAdditionalInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    string? SupportStatus,
    string? Description,
    string? PetColor,
    DateOnly? BirthDate,
    HealthDetailsDTO? HealthDetails,
    BiometricDetailsDTO? BiometricDetails);