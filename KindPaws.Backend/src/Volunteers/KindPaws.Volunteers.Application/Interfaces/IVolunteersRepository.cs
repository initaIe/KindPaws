using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Interfaces;

public interface IVolunteersRepository
{
    Task<Result<Volunteer, Error>> GetByEmailAddressAsync(
        EmailAddress emailAddress,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetByPhoneNumberAsync(
        PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetByIdAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);

    void Delete(Volunteer volunteer);
}