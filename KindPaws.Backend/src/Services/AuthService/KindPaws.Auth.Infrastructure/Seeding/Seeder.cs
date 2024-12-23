using KindPaws.Auth.Application.Factories;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.Auth.Infrastructure.Seeding;

public static class Seeder
{
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            RoleFactory.ForceCreateNew("User")
        );
    }
}