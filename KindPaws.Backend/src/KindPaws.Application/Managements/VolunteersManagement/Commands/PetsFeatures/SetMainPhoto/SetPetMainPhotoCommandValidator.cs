using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SetMainPhoto;

public class SetPetMainPhotoCommandValidator : AbstractValidator<SetPetMainPhotoCommand>
{
    public SetPetMainPhotoCommandValidator()
    {
        RuleFor(s => s.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(s => s.PetId)
            .MustBeValueObject(PetId.Create);

        RuleFor(s => s.Path)
            .MustBeValueObject(FilePath.Create);
    }
}