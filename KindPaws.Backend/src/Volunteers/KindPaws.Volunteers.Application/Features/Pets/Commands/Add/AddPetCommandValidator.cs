using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.Add;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(a => a.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(a => a.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(a => a.BreedId)
            .MustBeValueObject(BreedId.Create);

        RuleFor(a => a.Name)
            .MustBeValueObject(ShortAlphabeticString.Create);
    }
}