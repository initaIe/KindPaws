using KindPaws.Core.Abstractions.Markers;

namespace KindPaws.Accounts.Application.Features.Accounts.Queries.ValidateAccountPassword;

public record ValidateAccountPasswordQuery(string EmailAddress, string Password) : IQuery;