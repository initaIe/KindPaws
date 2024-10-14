using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.VolunteerHandlers.Delete.DTOs;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.Delete.Validators;

public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerRequestValidator()
    {
        RuleFor(d => d.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}