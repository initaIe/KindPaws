namespace KindPaws.Application.DTOs;

public record BiometricDetailsDTO(
    float? Height,
    float? Weight,
    string? Gender);