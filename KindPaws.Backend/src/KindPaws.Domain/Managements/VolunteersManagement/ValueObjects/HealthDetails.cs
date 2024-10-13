using System.Collections;
using System.Text.Json.Serialization;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record HealthDetails
{
    // ef core
    private HealthDetails()
    {
    }
    
    [JsonConstructor]
    public HealthDetails(
        MediumDescription? description,
        List<Vaccine>? vaccines,
        List<Disease>? diseases,
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
    public List<Vaccine> Vaccines { get; }
    public List<Disease> Diseases { get; }
    public HealthStatus? HealthStatus { get; }
    public bool? IsNeutered { get; }

    public static HealthDetails CreateNullable()
    {
        return new HealthDetails(null, null, null, null, null);
    }
}