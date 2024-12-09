using FluentValidation;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Validation;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(rc => rc.EmailAddress)
            .MustBeValueObject(EmailAddress.Create);
        
        RuleFor(rc => rc.Password)
            .MustBeValueObject(Password.Create);
    }
}