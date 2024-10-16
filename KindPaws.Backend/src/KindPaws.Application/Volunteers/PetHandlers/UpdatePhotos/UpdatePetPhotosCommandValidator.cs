using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.PetHandlers.UpdatePhotos;

public class UpdatePetPhotosCommandValidator : AbstractValidator<UpdatePetPhotosCommand>
{
    public UpdatePetPhotosCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
        
        RuleFor(u => u.PetId)
            .MustBeValueObject(PetId.Create);   
    }
}