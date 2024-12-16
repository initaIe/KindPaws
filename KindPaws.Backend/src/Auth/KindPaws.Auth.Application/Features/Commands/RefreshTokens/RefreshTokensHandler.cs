// using KindPaws.Accounts.Contracts;
// using KindPaws.Accounts.Contracts.Requests;
// using KindPaws.Auth.Application.Abstractions;
// using KindPaws.Auth.Contracts.Responses;
// using KindPaws.Core.Abstractions.Handlers;
// using KindPaws.SharedKernel.Others;
// using KindPaws.SharedKernel.Others.ErrorManagement;
// using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
//
// namespace KindPaws.Auth.Application.Features.Commands.RefreshTokens;
//
// public class RefreshTokensHandler : ICommandHandler<RefreshTokensResponse, RefreshTokensCommand>
// {
//     private readonly IAccountsContract _accountsContract;
//     private readonly ITokenProvider _tokenProvider;
//
//     public RefreshTokensHandler(
//         IAccountsContract accountsContract,
//         ITokenProvider tokenProvider)
//     {
//         _accountsContract = accountsContract;
//         _tokenProvider = tokenProvider;
//     }
//
//     public async Task<Result<RefreshTokensResponse, ErrorList>> HandleAsync(
//         RefreshTokensCommand command,
//         CancellationToken cancellationToken = default)
//     {
//         var isAccessTokenValid = await _tokenProvider.ValidateAccessTokenAsync(command.AccessToken);
//
//         if (isAccessTokenValid.IsFailure)
//             return isAccessTokenValid.Error.ToErrorList();
//
//         var tokenParseResult = _tokenProvider.ParseAccessToken(command.AccessToken);
//
//         if (tokenParseResult.IsFailure)
//             return tokenParseResult.Error.ToErrorList();
//
//         var refreshSessionGetResult = await _accountsContract.GetRefreshSessionByAccountId(
//             tokenParseResult.Value.AccountId,
//             cancellationToken);
//
//         if (refreshSessionGetResult.IsFailure)
//             return Errors.Auth.TokenIsInvalid().ToErrorList();
//
//         if (tokenParseResult.Value.Jti != refreshSessionGetResult.Value.Jti)
//             return Errors.Auth.TokenIsInvalid().ToErrorList();
//
//         await _accountsContract.DeleteRefreshSessionAsync(
//             refreshSessionGetResult.Value.AccountId,
//             refreshSessionGetResult.Value.Id,
//             cancellationToken);
//
//         var jti = Jti.CreateRandom();
//
//         var addRefreshSessionRequest = new AddRefreshSessionRequest(jti.Value);
//         var addRefreshSessionResult = await _accountsContract.AddRefreshSessionAsync(
//             refreshSessionGetResult.Value.AccountId,
//             addRefreshSessionRequest,
//             cancellationToken);
//
//         if (addRefreshSessionResult.IsFailure)
//             return addRefreshSessionResult.Error;
//
//         var accessToken = _tokenProvider.GenerateAccessToken(
//             refreshSessionGetResult.Value.AccountId,
//             jti.Value);
//
//         return new RefreshTokensResponse(accessToken, addRefreshSessionResult.Value);
//     }
// }

