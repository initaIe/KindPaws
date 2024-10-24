using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(a => a.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(a => a.SpecieId)
            .MustBeValueObject(SpecieId.Create);

        RuleFor(a => a.BreedId).NotEmpty();

        RuleFor(a => a.Name)
            .MustBeValueObject(ShortName.Create);
    }
}