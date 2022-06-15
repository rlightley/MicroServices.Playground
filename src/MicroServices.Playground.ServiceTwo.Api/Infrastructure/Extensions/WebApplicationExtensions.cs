namespace MicroServices.Playground.ServiceTwo.Api.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    internal static void MigrateDb(this IApplicationBuilder app)
    {
        var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

        if (serviceScopeFactory is null)
        {
            return;
        }
        using var scope = serviceScopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database;
        try
        {
            var pendingMigrations = db.GetPendingMigrations();
            if (!pendingMigrations.Any())
            {
                return;
            }
            Console.WriteLine("Migrating Db context");
            db.Migrate();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to migrate database {db.GetDbConnection().DataSource} {db.GetDbConnection().Database}", ex);
        }

    }
}
