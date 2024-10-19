using KindPaws.Application.DTOs;

namespace KindPaws.Application.Volunteers.PetsHandlers.UpdateAdditionalInfo;

public record UpdatePetAdditionalInfoCommand(
    Guid VolunteerId,
    Guid PetId,
    string? SupportStatus,
    string? Description,
    string? PetColor,
    DateOnly? BirthDate,
    HealthDetailsDTO? HealthDetails,
    BiometricDetailsDTO? BiometricDetails);