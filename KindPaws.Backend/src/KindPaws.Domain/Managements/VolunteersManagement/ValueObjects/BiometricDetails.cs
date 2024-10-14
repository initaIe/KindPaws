using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

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

    public static BiometricDetails CreateNullable()
    {
        return new BiometricDetails(null, null, null);
    }
}