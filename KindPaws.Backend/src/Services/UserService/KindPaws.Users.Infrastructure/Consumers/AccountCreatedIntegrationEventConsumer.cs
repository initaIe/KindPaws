using KindPaws.Auth.Contracts.Messaging;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.Core.MessageBox.Entities;
using KindPaws.Core.MessageBox.Factories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Users.Infrastructure.Consumers;

public class AccountCreatedIntegrationEventConsumer : IConsumer<AccountCreatedIntegrationEvent>
{
    private readonly IInBoxWriteDbContext _inBoxWriteDbContext;

    public AccountCreatedIntegrationEventConsumer(IInBoxWriteDbContext inBoxWriteDbContext)
    {
        _inBoxWriteDbContext = inBoxWriteDbContext;
    }

    public async Task Consume(ConsumeContext<AccountCreatedIntegrationEvent> context)
    {
        var isEventAlreadyExist = await _inBoxWriteDbContext.InBoxMessages.AnyAsync(
            m => m.Id == context.Message.EventId,
            context.CancellationToken);

        if (isEventAlreadyExist)
            return;

        var inBoxMessage = MessageFactory.CreateNewInBoxMessage(context.Message);

        await _inBoxWriteDbContext.InBoxMessages.AddAsync(
            inBoxMessage,
            context.CancellationToken);

        await _inBoxWriteDbContext.SaveChangesAsync(context.CancellationToken);
    }
}