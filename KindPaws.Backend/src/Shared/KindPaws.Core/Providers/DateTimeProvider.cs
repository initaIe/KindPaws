using KindPaws.Core.Abstractions;

namespace KindPaws.Core.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    private readonly TimeProvider _timeProvider;

    public DateTimeProvider(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public DateTimeOffset GetUtcNow() => _timeProvider.GetUtcNow();

    public DateTimeOffset GetLocalNow() => _timeProvider.GetLocalNow();
}