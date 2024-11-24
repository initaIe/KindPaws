using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.AccountRoles.Commands.Delete;

public record DeleteAccountRoleCommand(
    Guid AccountId,
    Guid AccountRoleId)
    : ICommand;