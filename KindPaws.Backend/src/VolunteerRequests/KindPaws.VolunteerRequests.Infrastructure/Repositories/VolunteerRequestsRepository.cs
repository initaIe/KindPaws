using KindPaws.Core.Abstractions.Database;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.VolunteerRequests.Domain.AggregateRoot;
using KindPaws.VolunteerRequests.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.VolunteerRequests.Infrastructure.Repositories;

public class VolunteerRequestsRepository : IRepository<VolunteerRequest, VolunteerRequestId>
{
    private readonly VolunteerRequestsWriteDbContext _dbContext;

    public VolunteerRequestsRepository(VolunteerRequestsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        VolunteerRequest volunteerRequest,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.VolunteerRequests.AddAsync(volunteerRequest, cancellationToken);
    }

    public void Delete(VolunteerRequest volunteerRequest)
    {
        _dbContext.VolunteerRequests.Remove(volunteerRequest);
    }

    public async Task<Result<VolunteerRequest, Error>> GetByIdAsync(
        VolunteerRequestId volunteerRequestId,
        CancellationToken cancellationToken = default)
    {
        var volunteerRequest = await _dbContext.VolunteerRequests
            .FirstOrDefaultAsync(vr => vr.Id == volunteerRequestId, cancellationToken);

        if (volunteerRequest == null)
            return ErrorsGeneral.RecordNotFound(
                nameof(VolunteerRequest),
                nameof(VolunteerRequestId),
                volunteerRequestId.Value);

        return volunteerRequest;
    }
}