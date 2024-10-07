using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Application.Volunteers.Create;

// TODO: move to controller and make DTOs mb
public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(c => c.FullName)
            .MustBeValueObject(x => FullName.Create(
                x.FirstName,
                x.LastName,
                x.Patronymic));

        RuleFor(c => c.EmailAddress).MustBeValueObject(EmailAddress.Create);

        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}