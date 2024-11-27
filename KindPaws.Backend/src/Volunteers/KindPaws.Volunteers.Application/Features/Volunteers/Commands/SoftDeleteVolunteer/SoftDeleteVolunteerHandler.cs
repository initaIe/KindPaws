using FluentValidation;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.SoftDeleteVolunteer;

// TODO: add delete pets photo minio
public class SoftDeleteVolunteerHandler
    : ICommandHandler<Guid, SoftDeleteVolunteerCommand>
{
    private readonly ILogger<SoftDeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeleteVolunteerCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public SoftDeleteVolunteerHandler(
        IRepository<Volunteer, VolunteerId> volunteersRepository,
        ILogger<SoftDeleteVolunteerHandler> logger,
        IValidator<SoftDeleteVolunteerCommand> validator,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SoftDeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        volunteerResult.Value.SoftDelete();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteerId);

        return volunteerId.Value;
    }

    private void Log(VolunteerId volunteerId)
    {
        _logger.LogInformation(
            "VOLUNTEER soft deleted with ID: {Id}",
            volunteerId);
    }
}