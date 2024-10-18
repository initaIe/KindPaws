using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.VolunteersHandlers.GetById;

public class GetVolunteerByIdCommandValidator : AbstractValidator<GetVolunteerByIdCommand>
{
    public GetVolunteerByIdCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);
    }
}