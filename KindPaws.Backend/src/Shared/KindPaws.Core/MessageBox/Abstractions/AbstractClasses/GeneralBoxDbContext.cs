using KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;
using KindPaws.Core.MessageBox.Abstractions.Interfaces;
using KindPaws.Core.MessageBox.Database;
using KindPaws.Core.MessageBox.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace KindPaws.Core.MessageBox.Abstractions.AbstractClasses;

public abstract class GeneralBoxDbContext : WriteDbContext, IGeneralBoxDbContext
{
    private readonly ISaveChangesInterceptor _saveChangesInterceptor;

    protected GeneralBoxDbContext(ISaveChangesInterceptor saveChangesInterceptor)
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxMessageConfiguration<InBoxMessage>).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxMessageConfiguration<OutBoxMessage>).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<InBoxMessage> InBoxMessages => Set<InBoxMessage>();
    public DbSet<OutBoxMessage> OutBoxMessages => Set<OutBoxMessage>();
}