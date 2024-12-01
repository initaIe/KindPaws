namespace KindPaws.Volunteers.Infrastructure.Options;

public class ExpiredEntitiesCleanerBackgroundServiceOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(ExpiredEntitiesCleanerBackgroundServiceOptions);

    public int IntervalInHours { get; init; }
}