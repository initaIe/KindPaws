using System.Text.Json;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.SharedKernel.Others;

namespace KindPaws.Auth.Infrastructure.OutBox;

public class OutBoxRepository : IOutBoxRepository
{
    private readonly AuthWriteDbContext _dbContext;

    public OutBoxRepository(AuthWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync<T>(
        IEnumerable<T> messages,
        CancellationToken cancellationToken = default)
        where T : IEvent
    {
        var outboxMessages = messages.Select(OutBoxMessage.CreateNew);
        await _dbContext.AddRangeAsync(outboxMessages, cancellationToken);
    }
}