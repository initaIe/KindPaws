using FluentValidation;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(rc => rc.UserName)
            .MustBeValueObject(UserName.Create);
        
        RuleFor(rc => rc.EmailAddress)
            .MustBeValueObject(EmailAddress.Create);
        
        RuleFor(rc => rc.Password)
            .MustBeValueObject(Password.Create);
    }
}