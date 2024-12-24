using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.Abstractions.Database.DbContexts;

public abstract class WriteDbContext : ApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        base.OnConfiguring(optionsBuilder);
    }
}