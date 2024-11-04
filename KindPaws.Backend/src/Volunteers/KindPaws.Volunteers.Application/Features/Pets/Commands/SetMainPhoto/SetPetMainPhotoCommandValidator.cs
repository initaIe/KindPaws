using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SetMainPhoto;

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