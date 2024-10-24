using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;

public class AddPetPhotosCommandValidator : AbstractValidator<AddPetPhotosCommand>
{
    public AddPetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(u => u.PetId)
            .MustBeValueObject(PetId.Create);
    }
}