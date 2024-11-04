using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.DeletePhotos;

public class DeletePetPhotosCommandValidator : AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(u => u.PetId)
            .MustBeValueObject(PetId.Create);

        RuleForEach(u => u.PhotosPaths)
            .MustBeValueObject(FilePath.Create);
    }
}