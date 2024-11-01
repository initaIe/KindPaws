using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

namespace KindPaws.Application.DTOs;

public record HealthDetailsDTO(
    string? Description,
    IEnumerable<string>? Vaccines,
    IEnumerable<string>? Diseases,
    string? HealthStatus,
    bool? IsNeutered)
{
    public static HealthDetailsDTO GetFromDomainModel(HealthDetails healthDetails)
        => new(
            healthDetails.Description?.Value,
            healthDetails.Vaccines?.Select(vaccine => vaccine.Value),
            healthDetails.Diseases?.Select(disease => disease.Value),
            healthDetails.HealthStatus?.Value,
            healthDetails.IsNeutered);
}