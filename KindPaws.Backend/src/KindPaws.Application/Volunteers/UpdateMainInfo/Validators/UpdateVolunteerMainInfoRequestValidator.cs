using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.UpdateMainInfo.Validators;

public class UpdateVolunteerMainInfoRequestValidator : AbstractValidator<UpdateVolunteerMainInfoRequest>
{
    public UpdateVolunteerMainInfoRequestValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}