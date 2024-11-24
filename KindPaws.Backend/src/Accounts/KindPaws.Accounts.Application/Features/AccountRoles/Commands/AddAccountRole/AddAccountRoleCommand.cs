using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.AccountRoles.Commands.AddAccountRole;

public record AddAccountRoleCommand(Guid AccountId, Guid RoleId) : ICommand;