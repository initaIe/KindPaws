using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record HealthDetails
{
    // ef core
    private HealthDetails()
    {
    }

    private HealthDetails(
        IEnumerable<Vaccine>? vaccines,
        IEnumerable<Disease>? diseases,
        MediumDescription? description,
        HealthStatus? healthStatus,
        bool? isNeutered)
    {
        Vaccines = vaccines?.ToList() ?? [];
        Diseases = diseases?.ToList() ?? [];
        Description = description ?? MediumDescription.CreateEmpty();
        HealthStatus = healthStatus ?? HealthStatus.CreateEmpty();
        IsNeutered = isNeutered;
    }

    public MediumDescription Description { get; }
    public IReadOnlyList<Vaccine> Vaccines { get; }
    public IReadOnlyList<Disease> Diseases { get; }
    public HealthStatus HealthStatus { get; }
    public bool? IsNeutered { get; }

    public static Result<HealthDetails, Error> Create(
        IEnumerable<Vaccine> vaccines,
        IEnumerable<Disease> diseases,
        MediumDescription description,
        HealthStatus healthStatus,
        bool isNeutered)
    {
        return new HealthDetails(
            vaccines,
            diseases,
            description,
            healthStatus,
            isNeutered);
    }

    public static HealthDetails CreateEmpty()
    {
        return new HealthDetails(
            null,
            null,
            null,
            null,
            null);
    }
}