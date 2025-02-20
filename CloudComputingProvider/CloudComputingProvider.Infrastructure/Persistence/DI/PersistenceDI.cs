using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudComputingProvider.Infrastructure.Persistence.DI
{
    public static class PersistenceDI
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions();
            //services.AddDbContext<CloudComputingProviderDBContext>(options =>
            //    options.UseSqlite(configuration.GetConnectionString("CloudComputingProviderDB"),
            //    b => b.MigrationsAssembly(typeof(CloudComputingProviderDBContext).Assembly.FullName)), ServiceLifetime.Transient);

            services.AddDbContext<CloudComputingProviderDBContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("CloudComputingProviderDB"));
            });
            return services;
        }
    }
}
