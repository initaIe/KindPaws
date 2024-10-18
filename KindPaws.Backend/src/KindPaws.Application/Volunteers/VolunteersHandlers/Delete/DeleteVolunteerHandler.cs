using FluentValidation;
using KindPaws.Application.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.VolunteersHandlers.Delete;

public class DeleteVolunteerHandler
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IValidator<DeleteVolunteerCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger,
        IValidator<DeleteVolunteerCommand> validator,
        IApplicationDbContext dbContext)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        _dbContext.Volunteers.Remove(volunteerResult.Value);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("VOLUNTEER soft deleted with ID: {VolunteerId}", volunteerId.Value);

        return volunteerId.Value;
    }
}