using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;

namespace KindPaws.Volunteers.Application.Interfaces;

public interface IVolunteersRepository
{
    Task<Result<Volunteer, Error>> GetByIdAsync(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Volunteer volunteer,
        CancellationToken cancellationToken = default);

    void Delete(Volunteer volunteer);
}