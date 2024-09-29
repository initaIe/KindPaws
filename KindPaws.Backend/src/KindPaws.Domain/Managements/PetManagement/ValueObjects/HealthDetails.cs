using KindPaws.Domain.Managements.PetManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.ValueObjects;

public record HealthDetails
{
    public HealthDetails()
    {
    }

    public HealthDetails(
        string description,
        List<Vaccine> vaccines,
        List<Disease> diseases,
        HealthStatus healthStatus,
        bool isNeutered)
    {
        Description = description;
        Vaccines = vaccines;
        Diseases = diseases;
        HealthStatus = healthStatus;
        IsNeutered = isNeutered;
    }

    public string Description { get; }
    public List<Vaccine> Vaccines { get; }
    public List<Disease> Diseases { get; }
    public HealthStatus HealthStatus { get; }
    public bool IsNeutered { get; }

    public static Result<HealthDetails, IEnumerable<string>> Create(
        string description,
        List<Vaccine> vaccines,
        List<Disease> diseases,
        HealthStatus healthStatus,
        bool isNeutered)
    {
        List<string> errors = [];

        description.DefaultValidate(
                HealthDetailsConstraints.MinLength,
                HealthDetailsConstraints.MaxLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new HealthDetails(
            description,
            vaccines,
            diseases,
            healthStatus,
            isNeutered);
    }
}