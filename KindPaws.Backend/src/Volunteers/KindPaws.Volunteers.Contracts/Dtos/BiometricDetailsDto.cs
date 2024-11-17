namespace KindPaws.Volunteers.Contracts.Dtos;

public record BiometricDetailsDto(
    float? Height,
    float? Weight,
    string? Gender);