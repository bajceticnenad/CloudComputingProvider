using CloudComputingProvider.DataModel.Domain.Models;
using CloudComputingProvider.Infrastructure.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CloudComputingProvider.Infrastructure.Persistence
{
    public class CloudComputingProviderDBContext : DbContext, ICloudComputingProviderDBContext
    {
        #region PrivateFields
        private readonly IConfiguration _configuration;
        #endregion PrivateFields

        #region Public Constructor
        public CloudComputingProviderDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public CloudComputingProviderDBContext(DbContextOptions<CloudComputingProviderDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        #endregion

        #region DbSet

        public DbSet<Customers> Customers { get; set; }
        public DbSet<CustomerAccounts> CustomerAccounts { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<SubscriptionDetails> SubscriptionDetails { get; set; }
        public DbSet<States> States { get; set; }

        #endregion

        #region Protected
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_configuration.GetConnectionString("CloudComputingProviderDB"));
            }
        }
        #endregion

        #region Public Methods
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion

    }
}
