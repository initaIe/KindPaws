using KindPaws.Core.MessageBox.Entities;
using KindPaws.Core.MessageBox.Factories;
using KindPaws.SharedKernel.DDD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KindPaws.Core.MessageBox.Interceptors;

public class DomainEventsToOutboxInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
            await ProcessDomainEventsToOutboxAsync(eventData.Context, cancellationToken);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static async Task ProcessDomainEventsToOutboxAsync(DbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        var aggregateRoots = dbContext.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(e => e.State is not (EntityState.Unchanged or EntityState.Detached))
            .Select(e => e.Entity)
            .ToList();

        var allDomainEvents = aggregateRoots
            .SelectMany(aggregateRoot => aggregateRoot.DomainEvents);

        var outboxMessages = allDomainEvents.Select(MessageFactory.CreateNewOutBoxMessage);

        await dbContext.Set<OutBoxMessage>().AddRangeAsync(outboxMessages, cancellationToken);

        aggregateRoots.ForEach(aggregateRoot => aggregateRoot.ClearDomainEvents());
    }
}