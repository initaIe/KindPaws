using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateAdditionalInfo;

public class UpdateVolunteerAdditionalInfoEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdateVolunteerAdditionalInfoExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public UpdateVolunteerAdditionalInfoEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        UpdateVolunteerAdditionalInfoExistenceValidationData validationData,
        CancellationToken cancellationToken)
    {
        var isVolunteerByIdExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        return true;
    }
}