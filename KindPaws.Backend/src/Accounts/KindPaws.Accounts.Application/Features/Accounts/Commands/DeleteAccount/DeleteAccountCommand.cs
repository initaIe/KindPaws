using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Commands.Delete;

public record DeleteAccountCommand(Guid AccountId) : ICommand;