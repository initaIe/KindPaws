using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record BiometricDetails
{
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