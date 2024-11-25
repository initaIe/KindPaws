using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Queries.GetAccountByEmailAddress;

public record GetAccountByEmailAddressQuery(string EmailAddress) : IQuery;