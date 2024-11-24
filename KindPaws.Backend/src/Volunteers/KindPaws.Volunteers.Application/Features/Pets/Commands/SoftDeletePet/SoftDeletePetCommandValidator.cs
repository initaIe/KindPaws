using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SoftDeletePet;

public class SoftDeletePetCommandValidator : AbstractValidator<SoftDeletePetCommand>
{
    public SoftDeletePetCommandValidator()
    {
        RuleFor(d => d.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(d => d.PetId)
            .MustBeValueObject(PetId.Create);
    }
}