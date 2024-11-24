using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.DeleteAccount;

public record DeleteAccountCommand(Guid AccountId) : ICommand;