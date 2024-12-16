// using KindPaws.Accounts.Contracts;
// using KindPaws.Accounts.Contracts.Requests;
// using KindPaws.Auth.Application.Abstractions;
// using KindPaws.Core.Abstractions.Handlers;
// using KindPaws.Roles.Contracts;
// using KindPaws.SharedKernel.Others;
// using KindPaws.SharedKernel.Others.ErrorManagement;
//
// namespace KindPaws.Auth.Application.Features.Commands.Register;
//
// public class RegisterHandler : ICommandHandler<Guid, RegisterCommand>
// {
//     private readonly IAccountsContract _accountsContract;
//     private readonly IAuthOptionsProvider _authOptionsProvider;
//     private readonly IRolesContract _rolesContract;
//
//     public RegisterHandler(
//         IAccountsContract accountsContract,
//         IAuthOptionsProvider authOptionsProvider,
//         IRolesContract rolesContract)
//     {
//         _accountsContract = accountsContract;
//         _authOptionsProvider = authOptionsProvider;
//         _rolesContract = rolesContract;
//     }
//
//     public async Task<Result<Guid, ErrorList>> HandleAsync(
//         RegisterCommand command,
//         CancellationToken cancellationToken = default)
//     {
//         var createAccountRequest = new CreateAccountRequest(command.UserName, command.EmailAddress, command.Password);
//         var accountCreationResult = await _accountsContract.CreateAccountAsync(createAccountRequest, cancellationToken);
//         if (accountCreationResult.IsFailure)
//             return accountCreationResult.Error;
//
//         var defaultRoleNameForNewAccount = _authOptionsProvider.GetDefaultAccountRoleName();
//
//         var getRoleIdByNameResult = await _rolesContract.GetRoleIdByNameAsync(
//             defaultRoleNameForNewAccount,
//             cancellationToken);
//
//         if (getRoleIdByNameResult.IsFailure)
//             return getRoleIdByNameResult.Error; // TODO: maybe add method AddRoleIfNotExist
//
//         var addAccountRoleRequest = new AddAccountRoleRequest(getRoleIdByNameResult.Value);
//         var addAccountRoleResult = await _accountsContract.AddAccountRoleAsync(
//             accountCreationResult.Value,
//             addAccountRoleRequest,
//             cancellationToken);
//
//         if (addAccountRoleResult.IsFailure)
//             return addAccountRoleResult.Error; // TODO: сделать так чтобы аккаунт создавался сразу с дефолтной ролью
//
//         return accountCreationResult.Value;
//     }
// }

