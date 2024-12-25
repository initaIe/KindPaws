using KindPaws.Core.Abstractions.Database.DbContexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;

public abstract class WriteDbContext : ApplicationDbContext, IWriteDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        base.OnConfiguring(optionsBuilder);
    }
}