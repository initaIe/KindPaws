using System.Runtime.InteropServices.JavaScript;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> AddAsync(Volunteer volunteer,
        CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByEmailAddressAsync(EmailAddress emailAddress,
        CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByPhoneNumberAsync(PhoneNumber phoneNumber,
        CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByIdAsync(VolunteerId volunteerId, 
        CancellationToken cancellationToken = default);
}