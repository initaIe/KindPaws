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

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.HardDeleteVolunteer;

// TODO: add delete pets photo minio
public class HardDeleteVolunteerHandler
    : ICommandHandler<Guid, HardDeleteVolunteerCommand>
{
    private readonly ILogger<HardDeleteVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<HardDeleteVolunteerCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public HardDeleteVolunteerHandler(
        ILogger<HardDeleteVolunteerHandler> logger,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IValidator<HardDeleteVolunteerCommand> validator,
        IRepository<Volunteer, VolunteerId> volunteersRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        HardDeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        _volunteersRepository.Delete(volunteerResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteerId);

        return volunteerId.Value;
    }

    private void Log(VolunteerId volunteerId)
    {
        _logger.LogInformation(
            "VOLUNTEER hard deleted with ID: {Id}",
            volunteerId);
    }
}