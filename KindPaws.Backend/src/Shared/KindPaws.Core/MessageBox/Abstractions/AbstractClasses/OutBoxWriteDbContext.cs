using KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.Core.MessageBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KindPaws.Core.MessageBox.Abstractions.AbstractClasses;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreAssemblyReference).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<OutBoxMessage> OutBoxMessages => Set<OutBoxMessage>();
}