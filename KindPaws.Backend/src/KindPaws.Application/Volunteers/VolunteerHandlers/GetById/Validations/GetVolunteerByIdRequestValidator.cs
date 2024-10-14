using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.VolunteerHandlers.GetById.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.GetById.Validations;

public class GetVolunteerByIdRequestValidator : AbstractValidator<GetVolunteerByIdRequest>
{
    public GetVolunteerByIdRequestValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}