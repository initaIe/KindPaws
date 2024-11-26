using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.RefreshSessions.Queries.GetRefreshSessionByAccountId;

public record GetRefreshSessionByAccountIdQuery(Guid AccountId) : IQuery;