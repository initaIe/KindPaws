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

    public string Description { get; private set; }
    public List<Vaccine> Vaccines { get; private set; }
    public List<Disease> Diseases { get; private set; }
    public HealthStatus HealthStatus { get; private set; }
    public bool IsNeutered { get; private set; }

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