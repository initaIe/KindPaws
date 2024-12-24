using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.Abstractions.Database.DbContexts;

public abstract class ReadDbContext : ApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}