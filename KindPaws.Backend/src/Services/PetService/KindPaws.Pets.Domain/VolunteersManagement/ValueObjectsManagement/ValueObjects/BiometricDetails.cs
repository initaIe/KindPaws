using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

public record BiometricDetails
{
    public BiometricDetails(
        Height? height,
        Weight? weight,
        Gender? gender,
        PetColor? color)
    {
        Height = height;
        Weight = weight;
        Gender = gender;
        Color = color;
    }

    public Height? Height { get; }
    public Weight? Weight { get; }
    public Gender? Gender { get; }
    public PetColor? Color { get; }
}