using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.Handlers.GetById.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.Handlers.GetById.Validations;

public class GetVolunteerByIdRequestValidator : AbstractValidator<GetVolunteerByIdRequest>
{
    public GetVolunteerByIdRequestValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}