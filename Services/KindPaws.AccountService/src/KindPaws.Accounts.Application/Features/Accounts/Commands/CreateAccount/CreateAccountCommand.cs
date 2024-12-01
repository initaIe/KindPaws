using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(
    string UserName,
    string EmailAddress,
    string Password)
    : ICommand;