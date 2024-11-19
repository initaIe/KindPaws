using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Domain.Account;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KindPaws.Accounts.Application.Features.Commands.Register;

public class RegisterHandler : ICommandHandler<RegisterCommand>
{
    private readonly ILogger<RegisterHandler> _logger;
    private readonly UserManager<Account> _userManager;

    public RegisterHandler(
        UserManager<Account> userManager,
        ILogger<RegisterHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }


    public async Task<Result<ErrorList>> HandleAsync(
        RegisterCommand command,
        CancellationToken cancellationToken = default)
    {
        // var user = new User
        // {
        //     Email = command.Email,
        //     UserName = command.UserName
        // };
        //
        // var creationResult = await _userManager.CreateAsync(user, command.Password);
        //
        // if (!creationResult.Succeeded)
        // {
        //     var errors = creationResult.Errors
        //         .Select(e => Error.Validation(e.Code, e.Description));
        //
        //     return new ErrorList(errors);
        // }
        //
        // _logger.LogInformation("Registered user with user name {UserName}", user.UserName);

        return true;
    }
}