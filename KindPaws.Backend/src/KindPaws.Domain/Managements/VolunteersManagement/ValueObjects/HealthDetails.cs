using System.Text.Json.Serialization;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record HealthDetails
{
    [JsonConstructor]
    private HealthDetails()
    {
    }

    public HealthDetails(
        MediumDescription? description,
        IEnumerable<Vaccine>? vaccines,
        IEnumerable<Disease>? diseases,
        HealthStatus? healthStatus,
        bool? isNeutered)
    {
        Description = description;
        Vaccines = vaccines?.ToList() ?? [];
        Diseases = diseases?.ToList() ?? [];
        HealthStatus = healthStatus;
        IsNeutered = isNeutered;
    }

    public MediumDescription? Description { get; }
    public IReadOnlyList<Vaccine> Vaccines { get; }
    public IReadOnlyList<Disease> Diseases { get; }
    public HealthStatus? HealthStatus { get; }
    public bool? IsNeutered { get; }

    public static HealthDetails CreateNullable()
    {
        return new HealthDetails(null, null, null, null, null);
    }
}