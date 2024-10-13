using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Application.Volunteers.UpdateMainInfo.Validators;

public class UpdateVolunteerMainInfoDTOValidator : AbstractValidator<UpdateVolunteerMainInfoDTO>
{
    public UpdateVolunteerMainInfoDTOValidator()
    {
        RuleFor(u => u.FullName)
            .MustBeValueObject(f => FullName.Create(
                f.FirstName,
                f.LastName,
                f.Patronymic));

        RuleFor(u => u.EmailAddress)
            .MustBeValueObject(EmailAddress.Create);

        RuleFor(u => u.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
    }
}