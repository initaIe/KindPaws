using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.Volunteer.UpdateAdditionalInfo.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.Volunteer.UpdateAdditionalInfo.Validators;

public class UpdateVolunteerAdditionalInfoRequestValidator : AbstractValidator<UpdateVolunteerAdditionalInfoRequest>
{
    public UpdateVolunteerAdditionalInfoRequestValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}