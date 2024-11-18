namespace KindPaws.Volunteers.Contracts.Dtos;

public record HealthDetailsDto(
    string? Description,
    IEnumerable<string> Vaccines,
    IEnumerable<string> Diseases,
    string? HealthStatus,
    bool? IsNeutered);