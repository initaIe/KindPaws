using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDelete;

public class HardDeleteVolunteerCommandValidator : AbstractValidator<HardDeleteVolunteerCommand>
{
    public HardDeleteVolunteerCommandValidator()
    {
        RuleFor(d => d.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}