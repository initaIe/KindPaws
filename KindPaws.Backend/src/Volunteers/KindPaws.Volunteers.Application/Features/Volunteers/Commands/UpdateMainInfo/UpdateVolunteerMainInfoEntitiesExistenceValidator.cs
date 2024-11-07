using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateMainInfo;

public class UpdateVolunteerMainInfoEntitiesExistenceValidator
    : IEntitiesExistenceValidator<UpdateVolunteerMainInfoExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public UpdateVolunteerMainInfoEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(
        UpdateVolunteerMainInfoExistenceValidationData validationData,
        CancellationToken cancellationToken = default)
    {
        var isVolunteerByIdExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.Id == validationData.VolunteerId, cancellationToken);
        if (!isVolunteerByIdExist)
            return Errors.General.RecordNotFound(nameof(Volunteer), nameof(VolunteerId), validationData.VolunteerId);

        var isVolunteerByEmailAddressExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.EmailAddress == validationData.EmailAddress, cancellationToken);
        if (isVolunteerByEmailAddressExist)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(EmailAddress));

        var isVolunteerByPhoneNumberExist = await _readDbContext.Volunteers.AnyAsync(
            v => v.PhoneNumber == validationData.PhoneNumber, cancellationToken);
        if (isVolunteerByPhoneNumberExist)
            return Errors.General.RecordAlreadyExist(nameof(Volunteer), nameof(PhoneNumber));

        return true;
    }
}