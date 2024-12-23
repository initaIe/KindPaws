using KindPaws.Core.OutBox.Abstractions;
using KindPaws.Core.OutBox.Entities;
using KindPaws.SharedKernel.DDD;

namespace KindPaws.Core.OutBox.Database;

public class OutBoxRepository : IOutBoxRepository
{
    private readonly IOutBoxWriteDbContext _writeDbContext;

    public OutBoxRepository(IOutBoxWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task AddRangeAsync<T>(
        IEnumerable<T> messages,
        CancellationToken cancellationToken = default)
        where T : IEvent
    {
        var outboxMessages = messages.Select(OutBoxMessage.CreateNew);
        await _writeDbContext.OutBoxMessages.AddRangeAsync(outboxMessages, cancellationToken);
    }
}