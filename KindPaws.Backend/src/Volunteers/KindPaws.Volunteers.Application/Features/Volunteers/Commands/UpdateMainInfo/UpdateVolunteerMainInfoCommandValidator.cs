using FluentValidation;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateMainInfo;

public class UpdateVolunteerMainInfoCommandValidator : AbstractValidator<UpdateVolunteerMainInfoCommand>
{
    public UpdateVolunteerMainInfoCommandValidator()
    {
        RuleFor(u => u.VolunteerId)
            .MustBeValueObject(VolunteerId.Create);

        RuleFor(u => u.FullName)
            .MustBeValueObject(f => FullName.Create(f.FirstName, f.LastName, f.Patronymic));

        RuleFor(u => u.EmailAddress)
            .MustBeValueObject(EmailAddress.Create);

        RuleFor(u => u.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
    }
}