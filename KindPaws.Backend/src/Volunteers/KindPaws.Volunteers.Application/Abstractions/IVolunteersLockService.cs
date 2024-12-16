using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Volunteers.Application.Abstractions;

public interface IVolunteersLockService
{
    Task SetVolunteerLockForUpdateAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);
}