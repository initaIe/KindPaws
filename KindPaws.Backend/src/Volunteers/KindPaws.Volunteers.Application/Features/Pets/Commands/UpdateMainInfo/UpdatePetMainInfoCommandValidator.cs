using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateMainInfo;

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
            .MustBeValueObject(ShortAlphabeticString.Create);
    }
}