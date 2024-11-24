using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.AccountRoles.Commands.DeleteAccountRole;

public record DeleteAccountRoleCommand(
    Guid AccountId,
    Guid AccountRoleId)
    : ICommand;