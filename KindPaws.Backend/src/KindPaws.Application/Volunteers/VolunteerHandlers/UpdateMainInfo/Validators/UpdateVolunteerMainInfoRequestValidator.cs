using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo.Validators;

public class UpdateVolunteerMainInfoRequestValidator : AbstractValidator<UpdateVolunteerMainInfoRequest>
{
    public UpdateVolunteerMainInfoRequestValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}