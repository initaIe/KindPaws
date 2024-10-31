using FluentValidation;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.SoftDelete;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;

public class HardDeleteVolunteerCommandValidator : AbstractValidator<HardDeleteVolunteerCommand>
{
    public HardDeleteVolunteerCommandValidator()
    {
        RuleFor(d => d.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}