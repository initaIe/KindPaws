using KindPaws.Core.Abstractions.Database.DbContexts.AbstractClasses;
using KindPaws.Pets.Application.Abstractions;
using KindPaws.Pets.Application.Common.DataModels;
using KindPaws.Pets.Infrastructure.Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KindPaws.Pets.Infrastructure.Persistence.DbContexts;

public class PetsReadDbContext : ReadDbContext, IPetsReadDbContext
{
    private readonly PostgresOptions _postgresOptions;

    public PetsReadDbContext(IOptions<PostgresOptions> postgresOptions)
    {
        _postgresOptions = postgresOptions.Value;
    }

    public override string GetSchemaName() => "pets";
    public override string GetConfigurationNamespace() => "Persistence.Configurations.Read";

    public IQueryable<VolunteerDataModel> Volunteers => Set<VolunteerDataModel>();
    public IQueryable<PetDataModel> Pets => Set<PetDataModel>();
    public IQueryable<SpecieDataModel> Species => Set<SpecieDataModel>();
    public IQueryable<BreedDataModel> Breeds => Set<BreedDataModel>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_postgresOptions.ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }
}