using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountByEmail;

public record ValidateAccountByQuery(string EmailAddress, string Password) : IQuery;