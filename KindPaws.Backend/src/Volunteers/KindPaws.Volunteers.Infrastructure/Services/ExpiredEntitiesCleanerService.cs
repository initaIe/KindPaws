using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Infrastructure.DbContexts;
using KindPaws.Volunteers.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindPaws.Volunteers.Infrastructure.Services;

public class ExpiredEntitiesCleanerService
{
    private readonly VolunteersWriteDbContext _dbContext;
    private readonly ILogger<ExpiredEntitiesCleanerService> _logger;
    private readonly ExpiredEntitiesCleanerServiceOptions _options;

    public ExpiredEntitiesCleanerService(
        VolunteersWriteDbContext dbContext,
        ILogger<ExpiredEntitiesCleanerService> logger,
        IOptions<ExpiredEntitiesCleanerServiceOptions> options)
    {
        _dbContext = dbContext;
        _logger = logger;
        _options = options.Value;
    }

    public async Task ProcessAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("ExpiredEntitiesCleanerService starting finding expired entities in db.");

        var volunteers = await GetVolunteersIncludePetsAsync(cancellationToken);

        foreach (var volunteer in volunteers)
            volunteer.DeleteExpiredPets(_options.PetLifeTimeAfterDeletionInDays);

        var expiredVolunteers = GetExpiredVolunteers(volunteers);

        _dbContext.Volunteers.RemoveRange(expiredVolunteers);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("ExpiredEntitiesCleanerService finished deleting expired entities files in db.");
    }

    private async Task<List<Volunteer>> GetVolunteersIncludePetsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Volunteers
            .Include(v => v.Pets)
            .ToListAsync(cancellationToken);
    }

    private List<Volunteer> GetExpiredVolunteers(IEnumerable<Volunteer> volunteers)
    {
        return volunteers.Where(v =>
                v.SoftDeletionTimestamp > DateTime.UtcNow.AddDays(-_options.VolunteerLifeTimeAfterDeletionInDays))
            .ToList();
    }
}