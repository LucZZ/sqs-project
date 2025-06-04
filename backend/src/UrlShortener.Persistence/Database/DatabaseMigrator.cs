using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UrlShortener.Persistence.Database;
public static class DatabaseMigrator {

    private const bool STOP_ON_ERROR = false;

    public static async Task MigrateDatabases(this WebApplication webApplication) {

        //var webApiOptions = webApplication.Configuration.GetOptions<WebAPIOptions>(WebAPIOptions.SectionName);

        //if (webApiOptions.USE_AUTO_MIGRATION && !webApiOptions.USE_IN_MEMORY_DATABASE) {
            using var scope = webApplication.Services.CreateScope();
            await scope.MigrateApplicationDatabase();
        //}
    }

    private static async Task MigrateApplicationDatabase(this IServiceScope serviceScope) {
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
        try {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any()) {
                logger.LogInformation("Applying {pendingMigrations} pending migrations for application database!", pendingMigrations.Count());
                await context.Database.MigrateAsync();
            }

            var lastAppliedMigration = (await context.Database.GetAppliedMigrationsAsync()).LastOrDefault() ?? "no latest migration";

            logger.LogInformation("Last schema version: {lastAppliedMigration}", lastAppliedMigration);
        } catch (Exception ex) {
            logger.LogWarning("Automatic migration was not successful! {Exception}", ex);
            if (STOP_ON_ERROR) {
                Environment.Exit(1);
            }
        }
    }
}