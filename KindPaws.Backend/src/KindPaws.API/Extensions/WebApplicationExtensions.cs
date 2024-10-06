using KindPaws.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KindPaws.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyMigration(this WebApplication webApplication)
    {
        try
        {
            await using var scope = webApplication.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}