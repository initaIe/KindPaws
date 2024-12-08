using FluentValidation;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Contracts.Responses;
using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Application.Features.Auth.Commands.Register;

public class RegisterHandler : ICommandHandler<RegisterResponse, RegisterCommand>
{
    private readonly IValidator<RegisterCommand> _commandValidator;
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IAuthReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    

    public RegisterHandler(
        IValidator<RegisterCommand> commandValidator,
        IAuthReadDbContext readDbContext, 
        IRepository<Account, AccountId> accountRepository,
        IUnitOfWork unitOfWork)
    {
        _commandValidator = commandValidator;
        _readDbContext = readDbContext;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterResponse, ErrorList>> HandleAsync(
        RegisterCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _commandValidator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

        try
        {
            var isUsernameOrEmailAddressAlreadyTaken = await _readDbContext.Accounts.AnyAsync(
                a=> a.UserName == command.UserName || a.EmailAddress == command.EmailAddress, 
                cancellationToken);

            if (isUsernameOrEmailAddressAlreadyTaken)
                return GeneralErrors.General.RecordAlreadyExist(nameof(Account)).ToErrorList();
            
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}