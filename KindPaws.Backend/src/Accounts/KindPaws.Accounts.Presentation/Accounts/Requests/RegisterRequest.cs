using KindPaws.Accounts.Application.Features.Commands.Register;

namespace KindPaws.Accounts.Presentation.Accounts.Requests;

public record RegisterRequest(
    string Email,
    string UserName,
    string Password)
{
    public RegisterCommand ToCommand()
        => new(Email, UserName, Password);
}