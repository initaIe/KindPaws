using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Abstractions.IoC;

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