namespace KindPaws.Volunteers.Contracts.Requests;

public record UpdatePetAdditionalInfoRequest(
    string? SupportStatus,
    string? Description,
    string? Color,
    DateTimeOffset? Birthday,
    HealthDetailsDto? HealthDetails,
    BiometricDetailsDto? BiometricDetails);