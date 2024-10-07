using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.Handlers.GetById.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.Handlers.GetById.Validators;

public class GetByIdVolunteerRequestValidator : AbstractValidator<GetByIdVolunteerRequest>
{
    public GetByIdVolunteerRequestValidator()
    {
        RuleFor(g => g.VolunteerId).MustBeValueObject(VolunteerId.Create);
    }
}