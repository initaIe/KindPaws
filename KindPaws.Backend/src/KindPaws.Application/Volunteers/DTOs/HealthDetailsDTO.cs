namespace KindPaws.Application.Volunteers.DTOs;

public record HealthDetailsDTO(
    string? Description,
    IEnumerable<string>? Vaccines,
    IEnumerable<string>? Diseases,
    string? HealthStatus,
    bool? IsNeutered);