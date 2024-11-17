namespace KindPaws.Core.Abstractions;

public interface IDateTimeProvider
{
    DateTimeOffset GetUtcNow();
    DateTimeOffset GetLocalNow();
}