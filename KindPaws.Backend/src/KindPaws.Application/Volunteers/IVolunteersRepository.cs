using System.Runtime.InteropServices.JavaScript;
using KindPaws.Domain.Managements.VolunteersManagement.AggregateRoot;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;

namespace KindPaws.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByEmailAddress(EmailAddress emailAddress);
    Task<Result<Volunteer, Error>> GetByPhoneNumber(PhoneNumber phoneNumber);
    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId);
}