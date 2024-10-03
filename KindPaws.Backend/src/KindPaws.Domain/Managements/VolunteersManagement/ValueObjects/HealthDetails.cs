using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record HealthDetails
{
    private readonly List<Disease> _diseases;
    private readonly List<Vaccine> _vaccines;

    // ef core
    private HealthDetails()
    {
    }

    private HealthDetails(
        List<Vaccine>? vaccines,
        List<Disease>? diseases,
        MediumDescription? description,
        HealthStatus? healthStatus,
        bool? isNeutered)
    {
        _vaccines = vaccines ?? [];
        _diseases = diseases ?? [];
        Description = description ?? MediumDescription.CreateEmpty();
        HealthStatus = healthStatus ?? HealthStatus.CreateEmpty();
        IsNeutered = isNeutered;
    }

    public MediumDescription Description { get; }
    public IReadOnlyList<Vaccine> Vaccines => _vaccines;
    public IReadOnlyList<Disease> Diseases => _diseases;
    public HealthStatus HealthStatus { get; }
    public bool? IsNeutered { get; }

    public static Result<HealthDetails, Error> Create(
        List<Vaccine> vaccines,
        List<Disease> diseases,
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