using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.HardDelete;

public class HardDeletePetCommandValidator : AbstractValidator<HardDeletePetCommand>
{
    public HardDeletePetCommandValidator()
    {
        RuleFor(d => d.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(d => d.PetId)
            .MustBeValueObject(PetId.Create);
    }
}