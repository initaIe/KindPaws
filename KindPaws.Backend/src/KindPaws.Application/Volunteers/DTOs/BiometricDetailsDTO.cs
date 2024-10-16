namespace KindPaws.Application.Volunteers.DTOs;

public record BiometricDetailsDTO(
    float? Height,
    float? Weight,
    string? Gender);