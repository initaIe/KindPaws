namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

public record HealthDetails
{
    public HealthDetails(
        HealthDetailsDescription? description,
        IEnumerable<Vaccine> vaccines,
        IEnumerable<Disease> diseases,
        HealthStatus? healthStatus,
        bool? isNeutered)
    {
        Description = description;
        Vaccines = vaccines.ToList();
        Diseases = diseases.ToList();
        HealthStatus = healthStatus;
        IsNeutered = isNeutered;
    }

    public HealthDetailsDescription? Description { get; }
    public IReadOnlyList<Vaccine> Vaccines { get; }
    public IReadOnlyList<Disease> Diseases { get; }
    public HealthStatus? HealthStatus { get; }
    public bool? IsNeutered { get; }
}