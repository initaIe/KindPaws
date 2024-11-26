namespace KindPaws.Volunteers.Infrastructure.Options;

public class ExpiredEntitiesCleanerServiceOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(ExpiredEntitiesCleanerServiceOptions);

    public int VolunteerLifeTimeAfterDeletionInDays { get; init; } 
    public int PetLifeTimeAfterDeletionInDays { get; init; } 
}