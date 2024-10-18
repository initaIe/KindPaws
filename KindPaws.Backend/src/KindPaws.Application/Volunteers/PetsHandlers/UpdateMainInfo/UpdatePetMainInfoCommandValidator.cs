using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.PetsHandlers.UpdateMainInfo;

public class UpdatePetMainInfoCommandValidator : AbstractValidator<UpdatePetMainInfoCommand>
{
    public UpdatePetMainInfoCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(u => u.PetId)
            .MustBeValueObject(PetId.Create);

        RuleFor(u => u.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(u => u.BreedId)
            .MustBeValueObject(BreedId.Create);

        RuleFor(u => u.Name)
            .MustBeValueObject(ShortName.Create);
    }
}