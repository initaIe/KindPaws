using FluentValidation;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Helpers;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;

public class CreateVolunteerHandler
    : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public CreateVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<CreateVolunteerCommand> validator,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var volunteer = VolunteerHelper.ForceCreateNewVolunteer(
            command.Description,
            command.Address,
            command.YearsOfExperience,
            command.Requisites);

        await _volunteersRepository.AddAsync(volunteer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteer.Id);

        return volunteer.Id.Value;
    }

    private void Log(VolunteerId volunteerId)
    {
        _logger.LogInformation("VOLUNTEER created with ID: {Id};",
            volunteerId);
    }
}