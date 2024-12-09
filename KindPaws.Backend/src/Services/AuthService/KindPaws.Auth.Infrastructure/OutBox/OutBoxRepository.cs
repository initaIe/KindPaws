using System.Text.Json;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.DbContexts;
using KindPaws.Core.Abstractions;
using KindPaws.SharedKernel.Others;
using MediatR;

namespace KindPaws.Auth.Infrastructure.OutBox;

public class OutBoxRepository : IOutBoxRepository
{
    private readonly AuthWriteDbContext _dbContext;

    public OutBoxRepository(AuthWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IEvent
    {
        var outboxMessage = new OutBoxMessage
        {
            Id = message.EventId,
            OccuredAt = message.OccurredAt,
            Type = message.EventType,
            Payload = JsonSerializer.Serialize(message)
        };

        await _dbContext.AddAsync(outboxMessage, cancellationToken);
    }
}