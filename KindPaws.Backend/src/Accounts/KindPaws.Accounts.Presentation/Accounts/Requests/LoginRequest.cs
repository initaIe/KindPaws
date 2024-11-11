using KindPaws.Accounts.Application.Features.Commands.Login;

namespace KindPaws.Accounts.Presentation.Accounts.Requests;

public record LoginRequest(
    string Email,
    string Password)
{
    public LoginCommand ToCommand()
        => new(Email, Password);
}