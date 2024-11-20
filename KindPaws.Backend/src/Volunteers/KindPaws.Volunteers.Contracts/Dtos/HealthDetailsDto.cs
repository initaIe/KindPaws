namespace KindPaws.Volunteers.Contracts.Dtos;

public record HealthDetailsDto
{
    public string? Description { get; init; }
    public IEnumerable<string> Vaccines { get; init; } = [];
    public IEnumerable<string> Diseases { get; init; } = [];
    public string? HealthStatus { get; init; }
    public bool? IsNeutered { get; init; }
}