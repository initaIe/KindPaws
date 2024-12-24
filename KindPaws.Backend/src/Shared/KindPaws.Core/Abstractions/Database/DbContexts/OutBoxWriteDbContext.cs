using KindPaws.Core.OutBox.Abstractions;
using KindPaws.Core.OutBox.Database;
using KindPaws.Core.OutBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KindPaws.Core.Abstractions.Database.DbContexts;

public abstract class OutBoxWriteDbContext : WriteDbContext, IOutBoxWriteDbContext
{
    private readonly ISaveChangesInterceptor _saveChangesInterceptor;

    protected OutBoxWriteDbContext(ISaveChangesInterceptor saveChangesInterceptor)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutBoxMessagesConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<OutBoxMessage> OutBoxMessages => Set<OutBoxMessage>();
}