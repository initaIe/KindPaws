using KindPaws.Accounts.Domain;
using KindPaws.Core;
using KindPaws.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindPaws.Accounts.Infrastructure;

public class AccountsWriteDbContext(IConfiguration configuration) : IdentityDbContext<User, Role, Guid>
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.Database.Postgres));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("accounts");

        modelBuilder.Entity<User>()
            .ToTable("users");

        modelBuilder.Entity<User>()
            .Property(u => u.SocialNetworks)
            .HasJsonConversion()
            .HasColumnType("jsonb");
            
        modelBuilder.Entity<Role>()
            .ToTable("roles");
        
        modelBuilder.Entity<Permission>()
            .ToTable("permissions");

        modelBuilder.Entity<Permission>()
            .HasIndex(p => p.Code)
            .IsUnique();
        
        modelBuilder.Entity<RolePermission>()
            .ToTable("role_permissions");
        
        modelBuilder.Entity<RolePermission>()
            .HasOne(r => r.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(r => r.RoleId);
        
        modelBuilder.Entity<RolePermission>()
            .HasOne(r => r.Permission)
            .WithMany()
            .HasForeignKey(r => r.PermissionId);

        modelBuilder.Entity<RolePermission>()
            .HasKey(r => new { r.RoleId, r.PermissionId });
        
        modelBuilder.Entity<IdentityUserClaim<Guid>>()
            .ToTable("user_claims");

        modelBuilder.Entity<IdentityUserToken<Guid>>()
            .ToTable("user_tokens");

        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("user_logins");

        modelBuilder.Entity<IdentityRoleClaim<Guid>>()
            .ToTable("role_claims");

        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .ToTable("user_roles");
        
        // modelBuilder.ApplyConfigurationsFromAssembly(
        //     typeof(AccountsWriteDbContext).Assembly,
        //     type => type.FullName?.Contains("Configurations.Write") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}