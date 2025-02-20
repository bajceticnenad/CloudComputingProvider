using CloudComputingProvider.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingProvider.Extensions.EF
{
    public static class Migration
    {
        public static async void ExecuteMigrations(this IApplicationBuilder app)
        {
            await using var scope = app.ApplicationServices.CreateAsyncScope();

            using var db = scope.ServiceProvider.GetService<CloudComputingProviderDBContext>();
            if (db != null)
            {
                await db.Database.MigrateAsync();
            }

            var dataSeed = scope.ServiceProvider.GetRequiredService<SeedData>();
            await dataSeed.SeedAsync();
        }
    }
}
