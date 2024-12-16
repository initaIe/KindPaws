using FluentValidation;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Core.Validation;

namespace KindPaws.Auth.Application.Features.Auth.Commands.RefreshTokens;

public class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensCommand>
{
    public RefreshTokensCommandValidator()
    {
        RuleFor(rt => rt.RefreshToken)
            .MustBeValueObject(RefreshToken.Create);
        
        // TODO: access token validation
    }
}