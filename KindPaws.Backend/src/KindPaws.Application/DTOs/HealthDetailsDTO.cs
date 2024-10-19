namespace KindPaws.Application.DTOs;

public record HealthDetailsDTO(
    string? Description,
    IEnumerable<string>? Vaccines,
    IEnumerable<string>? Diseases,
    string? HealthStatus,
    bool? IsNeutered);