using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdatePetPosition;

public class UpdatePetPositionCommandValidator : AbstractValidator<UpdatePetPositionCommand>
{
    public UpdatePetPositionCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(u => u.PetId)
            .MustBeValueObject(PetId.Create);

        RuleFor(u => u.Position)
            .MustBeValueObject(Position.Create);
    }
}