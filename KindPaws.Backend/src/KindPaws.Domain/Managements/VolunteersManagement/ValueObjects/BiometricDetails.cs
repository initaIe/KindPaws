using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validation.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record BiometricDetails
{
    // ef core
    private BiometricDetails()
    {
    }

    public BiometricDetails(
        Height? height, 
        Weight? weight,
        Gender? gender)
    {
        Height = height;
        Weight = weight;
        Gender = gender;
    }

    public Height? Height { get; }
    public Weight? Weight { get; }
    public Gender? Gender { get; }
}