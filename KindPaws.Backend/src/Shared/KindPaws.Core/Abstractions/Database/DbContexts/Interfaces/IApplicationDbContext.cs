namespace KindPaws.Core.Abstractions.Database.DbContexts.Interfaces;

public interface IApplicationDbContext
{
    string GetSchemaName();
    string GetConfigurationNamespace();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}