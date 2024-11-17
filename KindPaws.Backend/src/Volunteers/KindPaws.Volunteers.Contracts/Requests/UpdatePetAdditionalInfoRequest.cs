using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Contracts.Requests;

public record UpdatePetAdditionalInfoRequest(
    string? SupportStatus,
    string? Description,
    string? Color,
    DateOnly? BirthDate,
    HealthDetailsDto? HealthDetails,
    BiometricDetailsDto? BiometricDetails);