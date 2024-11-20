using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Helpers;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateInfo;

public class UpdateVolunteerInfoHandler
    : ICommandHandler<Guid, UpdateVolunteerInfoCommand>
{
    private readonly IEntitiesExistenceValidator<UpdateVolunteerInfoExistenceValidationData>
        _entitiesExistenceValidator;

    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerInfoCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public UpdateVolunteerInfoHandler(
        IRepository<Volunteer, VolunteerId> volunteersRepository,
        ILogger<CreateVolunteerHandler> logger,
        IValidator<UpdateVolunteerInfoCommand> validator,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<UpdateVolunteerInfoExistenceValidationData> entitiesExistenceValidator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdateVolunteerInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var entitiesExistenceValidationData = command.ToExistenceValidationData();
        var entitiesExistenceValidationResult = await _entitiesExistenceValidator
            .ValidateAsync(entitiesExistenceValidationData, cancellationToken);
        if (entitiesExistenceValidationResult.IsFailure)
            return entitiesExistenceValidationResult.Error.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        var description = ValueObjectsHelpers.CreateNullableValueObject(
            command.Description,
            MediumString.Create);

        var address = ValueObjectsHelpers.CreateNullableValueObject(
            command.Address,
            a => Address.Create(a.City, a.Street));

        var yearsOfExperience = ValueObjectsHelpers.CreateNullableValueObject(
            command.YearsOfExperience,
            y => YearsOfExperience.Create(y!.Value));

        var requisites = ValueObjectsHelpers.CreateNullableValueObjects(
            command.Requisites,
            r => Requisite.Create(r.Name, r.Description));

        volunteerResult.Value.UpdateInfo(description, address, yearsOfExperience, requisites);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteerId);

        return volunteerId.Value;
    }

    private void Log(VolunteerId volunteerId)
    {
        _logger.LogInformation("VOLUNTEER updated info with ID: {Id};",
            volunteerId);
    }
}