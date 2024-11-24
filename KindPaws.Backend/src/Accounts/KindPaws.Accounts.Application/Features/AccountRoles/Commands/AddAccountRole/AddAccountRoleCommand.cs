using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.AccountRoles.Commands.Add;

public record AddAccountRoleCommand(Guid AccountId, Guid RoleId) : ICommand;