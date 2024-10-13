using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.UpdateAdditionalInfo.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.UpdateAdditionalInfo.Validators;

public class UpdateVolunteerAdditionalInfoRequestValidator : AbstractValidator<UpdateVolunteerAdditionalInfoRequest>
{
    public UpdateVolunteerAdditionalInfoRequestValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}