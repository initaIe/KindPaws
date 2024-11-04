using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.FullName)
            .MustBeValueObject(f => FullName.Create(
                f.FirstName,
                f.LastName,
                f.Patronymic));

        RuleFor(c => c.EmailAddress).MustBeValueObject(EmailAddress.Create);

        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
    }
}