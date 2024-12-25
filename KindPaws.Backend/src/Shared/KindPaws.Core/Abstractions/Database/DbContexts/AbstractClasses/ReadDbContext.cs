using KindPaws.Core.Abstractions.Database.DbContexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;

public abstract class ReadDbContext : ApplicationDbContext, IReadDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}