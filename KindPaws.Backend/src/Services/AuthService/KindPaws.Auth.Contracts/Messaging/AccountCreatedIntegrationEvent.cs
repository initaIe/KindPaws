using System.Text.Json.Serialization;
using KindPaws.Core.Abstractions.IntegrationEvents;
using KindPaws.SharedKernel.DDD;

namespace KindPaws.Auth.Contracts.Messaging;

public record AccountCreatedIntegrationEvent(
    Guid EventId,
    DateTimeOffset EventOccurredAt,
    Guid AccountId,
    string Username,
    string EmailAddress) 
    : IntegrationEvent(EventId, EventOccurredAt)
{
    public static string RoutingKey => "account.created";
    public static string QueueName => "account-created-queue";
}