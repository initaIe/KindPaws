using KindPaws.Core.Abstractions.Validators;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.Create;

public class
    CreateVolunteerEntitiesExistenceValidator : IEntitiesExistenceValidator<CreateVolunteerExistenceValidationData>
{
    private readonly IVolunteersReadDbContext _readDbContext;

    public CreateVolunteerEntitiesExistenceValidator(IVolunteersReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<Error>> ValidateAsync(CreateVolunteerExistenceValidationData validationData,
        CancellationToken cancellationToken = default)
    {
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